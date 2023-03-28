using PlaylistMaker.Contexts;
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

        #endregion


        /// <summary>
        /// Главное окно
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            context = new MainWindowContext();

            DataContext = context;
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
    }
}