using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.ViewModels
{
    /// <summary>
    /// Визуальное представление файла
    /// </summary>
    internal class FileItemView : AViewModel
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


        /*private string folderName = string.Empty;
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
        }*/


        private FolderItemView folder = null;
        /// <summary>
        /// Директория
        /// </summary>
        internal FolderItemView Folder
        {
            get => folder;
            private set
            {
                if (folder != value)
                {
                    folder = value;
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


        protected override void GetStatus(string parameter = "")
        {
            throw new NotImplementedException();
        }
    }
}