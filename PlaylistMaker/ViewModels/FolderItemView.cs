using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlaylistMaker.ViewModels
{
    /// <summary>
    /// Визуальное представление директории
    /// </summary>
    public class FolderItemView : AViewModel
    {
        private string fullName = string.Empty;
        /// <summary>
        /// Полное имя директории
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


        private string relativeName = string.Empty;
        /// <summary>
        /// Относительное имя
        /// </summary>
        public string RelativeName
        {
            get => relativeName;
            private set
            {
                if(relativeName != value)
                {
                    relativeName = value;
                    OnPropertyChanged();
                }
            }
        }


        private string name = string.Empty;
        /// <summary>
        /// Имя директории
        /// </summary>
        public string Name
        {
            get => name;
            private set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }


        private ObservableCollection<FileItemView> files = new ObservableCollection<FileItemView>();
        /// <summary>
        /// Список вложенных файлов
        /// </summary>
        public ObservableCollection<FileItemView> Files
            => files;


        /// <summary>
        /// Визуальное представление директории
        /// </summary>
        /// <param name="folderForPlaylist">путь к папке, содержащей файл плей-листа</param>
        /// <param name="fullPath">путь к директории</param>
        /// <param name="addFiles">признак добавления всех файлов из директории</param>
        public FolderItemView(string folderForPlaylist, string fullPath, bool addFiles)
        {
            FullName = fullPath;

            GetStatus(folderForPlaylist);

            if (addFiles)
                AddFiles();
        }


        /// <summary>
        /// Получение статуса
        /// </summary>
        /// <param name="parameter">параметр
        /// <code>
        /// для директории, в качестве параметра передается путь к папке с файлом плей-листа
        /// </code>
        /// </param>
        protected override void GetStatus(string parameter = "")
        {
            // Проверка существования директории
            if (!Directory.Exists(FullName))
            {
                Status = ItemStatus.NotExist;
                Name = string.Empty;
                RelativeName = string.Empty;
                return;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(FullName);
            Name = directoryInfo.Name;

            // Проверка относительного пути
            if(FullName.IndexOf(parameter) != 0)
            {
                Status = ItemStatus.NotRelative;
                RelativeName = string.Empty;
                return;
            }

            // Получение отностительного пути
            string relative = FullName.Substring(parameter.Length);

            if(relative.Length == 1)
            {
                relative = string.Empty;
            }
            else if(relative.Length > 0)
            {
                if (relative[0] == '\\')
                    relative = relative.Substring(1);

                if (relative[relative.Length - 1] != '\\')
                    relative += '\\';
            }

            RelativeName = relative;
            Status = ItemStatus.Exist;
        }


        /// <summary>
        /// Добавление всех файлов из директории
        /// </summary>
        public void AddFiles()
        {
            if (Status != ItemStatus.Exist)
                return;

            string[] files = Directory.GetFiles(FullName);

            AddFiles(FileHelper.GetAllowedFiles(files));
        }


        /// <summary>
        /// Добавление файлов из директории
        /// </summary>
        /// <param name="files">список файлов</param>
        public void AddFiles(string[] files)
        {
            if (Status != ItemStatus.Exist)
                return;


        }
    }
}