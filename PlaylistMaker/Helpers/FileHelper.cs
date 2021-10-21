using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        // TODO
        internal static int GetDuration(string fileName)
        {
            return 0;
        }
    }
}