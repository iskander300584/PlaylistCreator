using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using PlaylistMaker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PlaylistMaker
{
    /// <summary>
    /// Главное окно
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Контекст данных главного окна
        /// </summary>
        private MainWindowContext context;


        #region Объявление команд

        /// <summary>
        /// Команда выбора папки для плейлиста
        /// </summary>
        public static RoutedUICommand SelectPlaylistFolder_Command = new RoutedUICommand("SelectPlaylistFolder", "SelectPlaylistFolder", typeof(MainWindow));


        /// <summary>
        /// Команда добавления папки с файлами
        /// </summary>
        public static RoutedUICommand AddFolder_Command = new RoutedUICommand("AddFolder", "AddFolder", typeof(MainWindow));


        /// <summary>
        /// Команда удаления выбранных файлов
        /// </summary>
        public static RoutedUICommand Remove_Command = new RoutedUICommand("Remove", "Remove", typeof(MainWindow));

        #endregion


        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            context = new MainWindowContext();

            DataContext = context;

            ItemStatusFabrique.Initialize();
        }


        /// <summary>
        /// Проверка возможноти выбрать папку
        /// </summary>
        private void SelectPlaylistFolder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null;
        }


        /// <summary>
        /// Выбрать папку для плейлиста
        /// </summary>
        private void SelectPlaylistFolder_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.SelectPlaylistFolder();
        }


        /// <summary>
        /// Проверка возможности добавить папку с файлами
        /// </summary>
        private void AddFolder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && context.FolderForPlayList != null && context.FolderForPlayList != "";
        }


        /// <summary>
        /// Добавить папку
        /// </summary>
        private void AddFolder_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.AddFolder();
        }


        /// <summary>
        /// Проверка возможности удалить файлы
        /// </summary>
        private void Remove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && listView.SelectedItems != null && listView.SelectedItems.Count > 0;
        }


        /// <summary>
        /// Удалить файлы
        /// </summary>
        private void Remove_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            List<FileItemView> list = new List<FileItemView>();
            foreach (object item in listView.SelectedItems)
                list.Add(item as FileItemView);

            context.RemoveItems(list);
        }


        /// <summary>
        /// Закрыть
        /// </summary>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (context != null && (context.FolderForPlayList == null || context.FolderForPlayList == ""))
                Close();
            else if ((bool)Ascon.Dialogs.Dialogs.QuestionMessage("Закрыть без сохранения?", this))
                Close();
        }
    }
}