using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using PlaylistMaker.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;


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
        /// Команда выбора файла плейлиста
        /// </summary>
        public static RoutedUICommand SelectPlaylistFile_Command = new RoutedUICommand("SelectPlaylist", "SelectPlaylist", typeof(MainWindow));


        /// <summary>
        /// Команда добавления папки с файлами
        /// </summary>
        public static RoutedUICommand AddFolder_Command = new RoutedUICommand("AddFolder", "AddFolder", typeof(MainWindow));


        /// <summary>
        /// Команда добавления файлов
        /// </summary>
        public static RoutedUICommand AddFile_Command = new RoutedUICommand("AddFile", "AddFile", typeof(MainWindow));


        /// <summary>
        /// Команда удаления выбранных файлов
        /// </summary>
        public static RoutedUICommand Remove_Command = new RoutedUICommand("Remove", "Remove", typeof(MainWindow));


        /// <summary>
        /// Команда сохранения
        /// </summary>
        public static RoutedUICommand Save_Command = new RoutedUICommand("Save", "Save", typeof(MainWindow));


        /// <summary>
        /// Команда закрытия без сохранения
        /// </summary>
        public static RoutedUICommand Quit_Command = new RoutedUICommand("Quit", "Quit", typeof(MainWindow));


        /// <summary>
        /// Команда выполнения сортировки
        /// </summary>
        public static RoutedUICommand Sort_Command = new RoutedUICommand("Sort", "Sort", typeof(MainWindow));

        #endregion


        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            context = new MainWindowContext(this);

            DataContext = context;

            ItemStatusFabrique.Initialize();
        }


        #region Обработчики команд

        #region Выбор папки

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

        #endregion

        #region Добавление папки

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

        #endregion

        #region Удаление

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

        #endregion

        #region Сохранение

        /// <summary>
        /// Проверка возможности сохранения
        /// </summary>
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && context.Items != null && context.Items.Count > 0 && !context.Items.Any(i => i.Status != ItemStatus.Exist);
        }


        /// <summary>
        /// Сохранение
        /// </summary>
        private void Save_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.Save();
        }

        #endregion

        #region Добавление файлов

        /// <summary>
        /// Проверка возможности добавления файлов
        /// </summary>
        private void AddFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && context.FolderForPlayList != null && context.FolderForPlayList != "";
        }


        /// <summary>
        /// Добавление файлов
        /// </summary>
        private void AddFile_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.AddFiles();
        }

        #endregion

        #region Выход без сохранения

        /// <summary>
        /// Проверка возможности закрыть без сохранения
        /// </summary>
        private void Quit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && context.Items != null && context.Items.Count > 0;
        }


        /// <summary>
        /// Закрыть без сохранения
        /// </summary>
        private void Quit_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.Quit();
        }

        #endregion

        #region Выбор плейлиста

        /// <summary>
        /// Проверка возможности выбора плейлиста
        /// </summary>
        private void SelectPlaylist_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null;
        }


        /// <summary>
        /// Выбор плейлиста
        /// </summary>
        private void SelectPlaylist_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.SelectPlaylistFile();
        }

        #endregion

        #region Сортировка

        /// <summary>
        /// Проверка возможности выполнения сортировки
        /// </summary>
        private void Sort_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = context != null && context.Items != null && context.Items.Count > 1;
        }


        /// <summary>
        /// Сортировка
        /// </summary>
        private void Sort_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            context.ReSortItems();
        }

        #endregion

        #endregion
    }
}