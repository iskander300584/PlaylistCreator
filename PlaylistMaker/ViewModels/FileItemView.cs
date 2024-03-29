﻿using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using System.IO;


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

                    GetStringDuration();
                }
            }
        }


        private string strDuration = string.Empty;
        /// <summary>
        /// Строковое представление длительности
        /// </summary>
        public string StrDuration
        {
            get => strDuration;
            private set
            {
                if(strDuration != value)
                {
                    strDuration = value;
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

            FileInfo fileInfo = new FileInfo(FullName);
            FileName = fileInfo.Name;

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
                Duration = 0;
                return;
            }

            // Проверка вложенности в папку
            if(Folder == null)
            {
                Status = ItemStatus.NotInFolder;
                Duration = FileHelper.GetDuration(FullName);
                return;
            }

            FileInfo fileInfo = new FileInfo(FullName);

            if (fileInfo.Directory.FullName.ToLower().TrimEnd('\\') != new DirectoryInfo(Folder.FullName).FullName.ToLower().TrimEnd('\\'))
            {
                Status = ItemStatus.NotInFolder;
                Duration = FileHelper.GetDuration(FullName);
                return;
            }

            if(Folder.Status == ItemStatus.NotRelative)
            {
                Status = ItemStatus.NotRelative;
                Duration = FileHelper.GetDuration(FullName);
                return;
            }

            Status = ItemStatus.Exist;
            Duration = FileHelper.GetDuration(FullName);
            ParseStatus();
        }


        /// <summary>
        /// Получить строковое представление длительности
        /// </summary>
        private void GetStringDuration()
        {
            if(Duration <= 0)
            {
                StrDuration = string.Empty;
                return;
            }

            int minutes = Duration / 60;

            int seconds = Duration - (minutes * 60);
            string _sec = seconds > 9? seconds.ToString() : "0" + seconds.ToString();

            StrDuration = $"{minutes}:{_sec}";
        }
    }
}