using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlaylistMaker.Contexts;
using PlaylistMaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistMaker.UnitTests
{
    /// <summary>
    /// Тестирование класса FolderItemView
    /// </summary>
    [TestClass]
    public class Test_FolderItemViev
    {
        /// <summary>
        /// Тестирование статуса Exist
        /// </summary>
        [TestMethod]
        public void Test_Status_IsExist()
        {
            FolderItemView folderItemView = new FolderItemView(@"E:\Music\", @"E:\Music\Рок\", false);
            Assert.AreEqual(ItemStatus.Exist, folderItemView.Status);
        }
    }
}
