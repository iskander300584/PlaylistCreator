using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaylistMaker.Contexts;
using PlaylistMaker.ViewModels;
using System.Linq;


namespace UnitTests
{
    /// <summary>
    /// Тестирование класса FolderItemView
    /// </summary>
    [TestClass]
    public class Test_FolderItemView
    {
        /// <summary>
        /// Тестирование статуса Exist
        /// </summary>
        [TestMethod]
        public void Test_Status_IsExist()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            Assert.AreEqual(ItemStatus.Exist, folderItemView.Status);
        }


        /// <summary>
        /// Тестирование статуса NotExist
        /// </summary>
        [TestMethod]
        public void Test_Status_IsNotExist()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, @"E:\Music\Rock\", false);
            Assert.AreEqual(ItemStatus.NotExist, folderItemView.Status);
        }


        /// <summary>
        /// Тестирование статуса NotRelative
        /// </summary>
        [TestMethod]
        public void Test_Status_IsNotRelative()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, @"E:\Share\", false);
            Assert.AreEqual(ItemStatus.NotRelative, folderItemView.Status);
        }


        /// <summary>
        /// Тестирование получения имени папки
        /// </summary>
        [TestMethod]
        public void Test_Name()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            Assert.AreEqual(@"Rock", folderItemView.Name);
        }


        /// <summary>
        /// Тестирование получения относительного имени папки
        /// </summary>
        [TestMethod]
        public void Test_RelativeName()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            Assert.AreEqual(@"Rock", folderItemView.RelativeName);
        }


        /// <summary>
        /// Тестирование автоматического добавления списка файлов 
        /// </summary>
        [TestMethod]
        public void Test_AutoAddFiles()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, true);
            Assert.AreEqual(Constants.FILES.Length, folderItemView.Files.Count, "Количество добавленных файлов не соответствует");

            foreach (string file in Constants.FILES)
            {
                bool fileExist = folderItemView.Files.Any(f => f.FullName.ToLower() == file.ToLower());
                Assert.AreEqual(true, fileExist, $"Файл {file} не добавлен");
            }
        }


        /// <summary>
        /// Тестирование ручного добавления списка файлов 
        /// </summary>
        [TestMethod]
        public void Test_ManualAddFiles()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);

            string[] files = { Constants.FILE_1_PATH, Constants.FILE_2_PATH };
            folderItemView.AddFiles(files);

            Assert.AreEqual(files.Length, folderItemView.Files.Count, "Количество добавленных файлов не соответствует");

            foreach (string file in files)
            {
                bool fileExist = folderItemView.Files.Any(f => f.FullName.ToLower() == file.ToLower());
                Assert.AreEqual(true, fileExist, $"Файл {file} не добавлен");
            }
        }
    }
}