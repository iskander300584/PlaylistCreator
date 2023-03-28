using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace PlaylistMaker.ViewModels
{
    /// <summary>
    /// Визуальное представление файла
    /// </summary>
    public class FileItemView : AViewModel
    {
        private string fullName = string.Empty;
        /// <summary>
        /// Полное имя файла
        /// </summary>
        public string FullName
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
        public string FileName
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


        private FolderItemView folder = null;
        /// <summary>
        /// Директория
        /// </summary>
        public FolderItemView Folder
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
        public int Duration
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


        /// <summary>
        /// Визуальное представление файла
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="folderItem">папка</param>
        public FileItemView(string path, FolderItemView folderItem)
        {
            Folder = folderItem;
            FullName = path;

            GetStatus();
        }


        /// <summary>
        /// Получение статуса
        /// </summary>
        /// <param name="parameter">параметр
        /// <code>
        /// для файла параметр пустой
        /// </code>
        /// </param>
        protected override void GetStatus(string parameter = "")
        {
            // Проверка существования файла
            if(!File.Exists(FullName))
            {
                Status = ItemStatus.NotExist;
                FileName = string.Empty;
                Duration = 0;
                return;
            }

            FileInfo fileInfo = new FileInfo(FullName);
            FileName = fileInfo.Name;

            // Проверка вложенности в папку
            if(Folder == null)
            {
                Status = ItemStatus.NotInFolder;
                Duration = 0;
                return;
            }

            if(fileInfo.Directory.FullName.ToLower() != new DirectoryInfo(Folder.FullName).FullName.ToLower())
            {
                Status = ItemStatus.NotInFolder;
                Duration = 0;
                return;
            }

            Status = ItemStatus.Exist;
            Duration = FileHelper.GetDuration(FullName);
        }
    }
}