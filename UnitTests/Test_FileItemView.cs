using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaylistMaker.Contexts;
using PlaylistMaker.ViewModels;


namespace UnitTests
{
    /// <summary>
    /// Тестирование класса FileItemView
    /// </summary>
    [TestClass]
    public class Test_FileItemView
    {
        /// <summary>
        /// Тестирование статуса Exist
        /// </summary>
        [TestMethod]
        public void Test_Status_IsExist()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            FileItemView fileItemView = new FileItemView(Constants.FILE_1_PATH, folderItemView);
            Assert.AreEqual(ItemStatus.Exist, fileItemView.Status);
        }


        /// <summary>
        /// Тестирование статуса NotExist
        /// </summary>
        [TestMethod]
        public void Test_Status_IsNotExist()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            FileItemView fileItemView = new FileItemView(@"E:\TestPlayerCreator\test.mp3", folderItemView);
            Assert.AreEqual(ItemStatus.NotExist, fileItemView.Status);
        }


        /// <summary>
        /// Тестирование статуса NotInFolder
        /// </summary>
        [TestMethod]
        public void Test_Status_IsNotInFolder()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            FileItemView fileItemView = new FileItemView(@"E:\TestPlaylistCreator\Queen - Save me.mp3", folderItemView);
            Assert.AreEqual(ItemStatus.NotInFolder, fileItemView.Status);
        }


        /// <summary>
        /// Тестирование статуса NotInFolder, директория null
        /// </summary>
        [TestMethod]
        public void Test_Status_IsNotInFolder_EmplyFolder()
        {
            FileItemView fileItemView = new FileItemView(@"E:\TestPlaylistCreator\Queen - Save me.mp3", null);
            Assert.AreEqual(ItemStatus.NotInFolder, fileItemView.Status);
        }


        /// <summary>
        /// Тестирование получения имени файла
        /// </summary>
        [TestMethod]
        public void Test_FileName()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);
            FileItemView fileItemView = new FileItemView(Constants.FILE_1_PATH, folderItemView);
            Assert.AreEqual(@"queen - killer queen.mp3", fileItemView.FileName.ToLower());
        }


        /// <summary>
        /// Тестирование получения длительности файла
        /// </summary>
        [TestMethod]
        public void Test_Duration()
        {
            FolderItemView folderItemView = new FolderItemView(Constants.PATH_TO_PLAYLIST_FILE, Constants.PATH_TO_MUSIC_FOLDER, false);

            FileItemView fileItemView = new FileItemView(Constants.FILE_1_PATH, folderItemView);
            Assert.AreEqual(181, fileItemView.Duration, $"Длительность файла {Constants.FILE_1_PATH} не совпадает");

            fileItemView = new FileItemView(Constants.FILE_2_PATH, folderItemView);
            Assert.AreEqual(228, fileItemView.Duration, $"Длительность файла {Constants.FILE_2_PATH} не совпадает");

            fileItemView = new FileItemView(Constants.FILE_3_PATH, folderItemView);
            Assert.AreEqual(182, fileItemView.Duration, $"Длительность файла {Constants.FILE_3_PATH} не совпадает");
        }
    }
}