using PlaylistMaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PlaylistMaker.Contexts
{
    /// <summary>
    /// Контекст данных главного окна
    /// </summary>
    internal class MainWindowContext : INotifyPropertyChanged
    {
        /// <summary>
        /// Окно
        /// </summary>
        private MainWindow window;


        private string folderForPlaylist = string.Empty;
        /// <summary>
        /// Путь к папке плейлиста
        /// </summary>
        internal string FolderForPlayList
        {
            get => folderForPlaylist;
            set
            {
                if(folderForPlaylist != value)
                {
                    folderForPlaylist = value;
                    OnPropertyChanged();

                    GetPlaylistVisibleName();
                }
            }
        }


        private string playlistFileName = string.Empty;
        /// <summary>
        /// Имя файла плей-листа
        /// </summary>
        internal string PlaylistFileName
        {
            get => playlistFileName;
            private set
            {
                if(playlistFileName != value)
                {
                    playlistFileName = value;
                    OnPropertyChanged();

                    GetPlaylistVisibleName();
                }
            }
        }

        
        private string playListVisibleName = string.Empty;
        /// <summary>
        /// Отображаемое имя плей-листа
        /// </summary>
        public string PlayListVisibleName
        {
            get => playListVisibleName;
            private set
            {
                if(playListVisibleName != value)
                {
                    playListVisibleName = value;
                    OnPropertyChanged();
                }
            }
        }


        private int sortIndex = 0;
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public int SortIndex
        {
            get => sortIndex;
            set
            {
                if(sortIndex != value)
                {
                    sortIndex = value;
                    OnPropertyChanged();

                    ReSortItems();
                }
            }
        }


        private ObservableCollection<FileItemView> items = new ObservableCollection<FileItemView>();
        /// <summary>
        /// Список файлов
        /// </summary>
        public ObservableCollection<FileItemView> Items
        { 
            get => items;
            private set
            {
                if(items != value)
                {
                    items = value;
                    OnPropertyChanged();
                }
            }
        }



        private List<FileItemView> selectedItems = null;
        /// <summary>
        /// Список выбранных файлов
        /// </summary>
        public List<FileItemView> SelectedItems
        {
            get => selectedItems;
            set
            {
                if(selectedItems != value)
                {
                    selectedItems = value;
                    OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Контекст данных главного окна
        /// </summary>
        internal MainWindowContext(MainWindow window)
        {
            this.window = window;

            GetPlaylistVisibleName();
        }


        /// <summary>
        /// Получение отображаемого имени плейлиста
        /// </summary>
        private void GetPlaylistVisibleName()
        {
            PlayListVisibleName = (PlaylistFileName != "") ? PlaylistFileName : FolderForPlayList;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Изменение свойства
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        /// <summary>
        /// Выбрать папку для плейлиста
        /// </summary>
        internal void SelectPlaylistFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            Items.Clear();

            FolderForPlayList = dialog.SelectedPath;
        }


        // TOOD
        internal void SelectPlaylistFile()
        {
            // выбрать файл плейлист
            // загрузить данные плейлиста на форму
        }


        /// <summary>
        /// Добавить папку
        /// </summary>
        internal void AddFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = FolderForPlayList;
            dialog.ShowNewFolderButton = false;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FolderItemView folderItem = new FolderItemView(FolderForPlayList, dialog.SelectedPath, true);
            if (folderItem.Files.Count > 0 && folderItem.Status == ItemStatus.Exist)
                foreach (FileItemView file in folderItem.Files)
                    AddItem(file);
        }


        /// <summary>
        /// Добавить файл в список
        /// </summary>
        /// <param name="item">файл</param>
        private void AddItem(FileItemView item)
        {
            if (Items.Count == 0)
            {
                Items.Add(item);
                return;
            }

            if (Items.Any(i => i.FileName == item.FileName && i.Folder.Name == item.Folder.Name))
                return;

            int index = Items.Count;

            if(SortIndex == 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    int folderCompare = String.Compare(Items[i].Folder.Name, item.Folder.Name);

                    if (folderCompare < 0)
                        continue;
                    else if(folderCompare > 0)
                    {
                        index = i;
                        break;
                    }

                    if (String.Compare(Items[i].FileName, item.FileName) > 0)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else
            {
                for(int i = 0; i < Items.Count; i++)
                {
                    if(String.Compare(Items[i].FileName, item.FileName) > 0)
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index == Items.Count)
                Items.Add(item);
            else
                Items.Insert(index, item);
        }


        /// <summary>
        /// Добавить файлы
        /// </summary>
        internal void AddFiles()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                InitialDirectory = FolderForPlayList,
                CheckFileExists = true,
                Filter = "Звуковые файлы|*.mp3;*.wav;*.wma;",
                FilterIndex = 0,
                Multiselect = true
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            FileInfo fileInfo = new FileInfo(dialog.FileNames[0]);
            FolderItemView folderItem = new FolderItemView(FolderForPlayList, fileInfo.DirectoryName, false);
            if(folderItem.Status == ItemStatus.Exist)
            {
                foreach (string fileName in dialog.FileNames)
                {
                    FileItemView file = new FileItemView(fileName, folderItem);
                    AddItem(file);
                }
            }
        }


        /// <summary>
        /// Пересортировать файлы
        /// </summary>
        private void ReSortItems()
        {
            if (Items.Count < 2)
                return;

            List<FileItemView> _temp = new List<FileItemView>();

            if (SortIndex == 0)
                _temp = Items.OrderBy(i => i.Folder.Name).ThenBy(i => i.FileName).ToList();
            else
                _temp = Items.OrderBy(i => i.FileName).ToList();

            Items = new ObservableCollection<FileItemView>();
            foreach (FileItemView item in _temp)
                Items.Add(item);
        }


        /// <summary>
        /// Удаление выбранных файлов
        /// </summary>
        /// <param name="list">список файлов</param>
        internal void RemoveItems(List<FileItemView> list)
        {
            for (int i = 0; i < list.Count; i++)
                Items.Remove(list[i]);
        }


        /// <summary>
        /// Сохранение плейлиста
        /// </summary>
        internal void Save()
        {
            // Получение имени плейлиста
            if (PlaylistFileName == null || PlaylistFileName == "")
            {
                PlaylistNameWindow _nameWind = new PlaylistNameWindow(window);
                if (!(bool)_nameWind.ShowDialog())
                    return;

                PlaylistFileName = $"{_nameWind.NAME}.m3u";
            }    

            string fileName = Path.Combine(FolderForPlayList, PlaylistFileName);
            FileInfo fileInfo = new FileInfo(fileName);
            using (StreamWriter stream = new StreamWriter(fileInfo.Create(), Encoding.UTF8))
            {
                stream.WriteLine(@"#EXTM3U");

                foreach (FileItemView file in Items)
                    WriteItem(file, stream);

                stream.Close();
            }

            Ascon.Dialogs.Dialogs.InfoMessage("Сохранение выполнено успешно", window);
        }


        /// <summary>
        /// Добавление записи о файле
        /// </summary>
        /// <param name="file">файл</param>
        /// <param name="stream">поток записи</param>
        private void WriteItem(FileItemView file, StreamWriter stream)
        {
            int index = file.FileName.LastIndexOf('.');
            if (index == -1)
                return;

            string name = file.FileName.Substring(0, index);

            string title = $@"#EXTINF:{file.Duration},{name}";
            stream.WriteLine(title);

            string path = Path.Combine(file.Folder.RelativeName, file.FileName);
            stream.WriteLine(path);
        }
    }
}