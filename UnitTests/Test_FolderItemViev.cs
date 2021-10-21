using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaylistMaker.Contexts;
using PlaylistMaker.ViewModels;


namespace UnitTests
{
    [TestClass]
    public class Test_FolderItemViev
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
            Assert.AreEqual(@"Rock\", folderItemView.RelativeName);
        }
    }
}