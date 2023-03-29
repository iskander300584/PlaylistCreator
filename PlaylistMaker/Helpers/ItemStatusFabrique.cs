using PlaylistMaker.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace PlaylistMaker.Helpers
{
    /// <summary>
    /// Фабрика пиктограмм статусов
    /// </summary>
    internal static class ItemStatusFabrique
    {
        /// <summary>
        /// Словарь сопоставления статусов с пиктограммами
        /// </summary>
        private static Dictionary<ItemStatus, BitmapImage> list = new Dictionary<ItemStatus, BitmapImage>();


        /// <summary>
        /// Инициализация фабрики
        /// </summary>
        internal static void Initialize()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileInfo fileInfo = new FileInfo(assembly.Location);

            string imagesPath = Path.Combine(fileInfo.DirectoryName, "Images");

            string filePath = Path.Combine(imagesPath, "error.png");
            if(File.Exists(filePath))
            {
                BitmapImage image = GetImageFromFile(filePath);

                list.Add(ItemStatus.NotExist, image);
            }

            filePath = Path.Combine(imagesPath, "folder.png");
            if (File.Exists(filePath))
            {
                BitmapImage image = GetImageFromFile(filePath);

                list.Add(ItemStatus.NotInFolder, image);
            }

            filePath = Path.Combine(imagesPath, "warning.png");
            if (File.Exists(filePath))
            {
                BitmapImage image = GetImageFromFile(filePath);

                list.Add(ItemStatus.NotRelative, image);
            }

            filePath = Path.Combine(imagesPath, "sucsess.png");
            if (File.Exists(filePath))
            {
                BitmapImage image = GetImageFromFile(filePath);

                list.Add(ItemStatus.Exist, image);
            }
        }


        /// <summary>
        /// Получить изображение
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        private static BitmapImage GetImageFromFile(string filePath)
        {
            try
            {
                BitmapImage img = new BitmapImage();

                byte[] bytes = File.ReadAllBytes(filePath);
                using (Stream stream = new MemoryStream(bytes))
                {
                    img.BeginInit();
                    img.StreamSource = stream;
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.EndInit();
                    img.Freeze();
                }

                return img;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Получить изображение
        /// </summary>
        /// <param name="status">статус</param>
        internal static BitmapImage GetImage(ItemStatus status)
        {
            return list[status];
        }
    }
}