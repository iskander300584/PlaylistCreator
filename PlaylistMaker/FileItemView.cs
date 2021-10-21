using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker
{
    internal class FileItemView : INotifyPropertyChanged
    {
        private string fullName = string.Empty;
        /// <summary>
        /// Полное имя файла
        /// </summary>
        internal string FullName
        {
            get => fullName;
            private set
            {
                if(fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged();
                }
            }
        }


        private string fileName = string.Empty;
        /// <summary>
        /// Имя файла
        /// </summary>
        internal string FileName
        {
            get => fileName;
            private set
            {
                if(fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged();
                }
            }
        }


        private string folderName = string.Empty;
        /// <summary>
        /// Имя директории
        /// </summary>
        internal string FolderName
        {
            get => folderName;
            private set
            {
                if (folderName != value)
                {
                    folderName = value;
                    OnPropertyChanged();
                }
            }
        }


        private int duration = 0;
        /// <summary>
        /// Длительность
        /// </summary>
        internal int Duration
        {
            get => duration;
            private set
            {
                if(duration != value)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
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
    }
}