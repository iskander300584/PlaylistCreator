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
        internal MainWindowContext()
        {
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
    }
}