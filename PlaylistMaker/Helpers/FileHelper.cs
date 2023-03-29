using System.Collections.Generic;
using System.IO;


namespace PlaylistMaker.Helpers
{
    /// <summary>
    /// Помощник работы с файлами
    /// </summary>
    internal class FileHelper
    {
        /// <summary>
        /// Список доступных расширений
        /// </summary>
        internal static List<string> Extensions = new List<string>()
        {
            ".mp3",
            ".wav",
            ".wma"
        };


        /// <summary>
        /// Получить список допустимых файлов
        /// </summary>
        /// <param name="files">список файлов</param>
        /// <returns>возвращает список файлов с допустимыми расширениями</returns>
        internal static string[] GetAllowedFiles(string[]  files)
        {
            List<string> allowed = new List<string>();

            foreach(string fileName in files)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (Extensions.Contains(fileInfo.Extension.ToLower()))
                    allowed.Add(fileName);
            }

            return allowed.ToArray();
        }


        /// <summary>
        /// Получить продолжительность файла
        /// </summary>
        /// <param name="fileName">имя файла</param>
        internal static int GetDuration(string fileName)
        {
            try
            {
                TagLib.File tFile = TagLib.File.Create(fileName);

                if (tFile != null)
                    return (int)tFile.Properties.Duration.TotalSeconds;
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Получение полного имени директории
        /// </summary>
        /// <param name="fileName">имя файла</param>
        internal static string GetFolderFullName(string fileName)
        {
            if (!File.Exists(fileName))
                return string.Empty;

            FileInfo fileInfo = new FileInfo(fileName);
            string directory = fileInfo.DirectoryName;

            if (directory[directory.Length - 1] != '\\')
                directory += '\\';

            return directory;
        }
    }
}