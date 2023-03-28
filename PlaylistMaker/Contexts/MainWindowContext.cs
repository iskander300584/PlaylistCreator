using PlaylistMaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                }
            }
        }


        private ObservableCollection<FileItemView> items = new ObservableCollection<FileItemView>();
        /// <summary>
        /// Список файлов
        /// </summary>
        private ObservableCollection<FileItemView> Items
            => items;


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
            dialog.ShowNewFolderButton = true;

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
    }
}
