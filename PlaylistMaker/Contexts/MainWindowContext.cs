using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


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
                }
            }
        }


        //private ObservableCollection<FileItemView> items = new ObservableCollection<FileItemView>();


        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Изменение свойства
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
