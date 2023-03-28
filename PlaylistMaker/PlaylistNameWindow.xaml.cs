using System.Windows;


namespace PlaylistMaker
{
    /// <summary>
    /// Окно задания имени плейлиста
    /// </summary>
    public partial class PlaylistNameWindow : Window
    {
        /// <summary>
        /// Имя плейлиста
        /// </summary>
        public string NAME { get; private set; } = string.Empty;


        /// <summary>
        /// Окно задания имени плейлиста
        /// </summary>
        /// <param name="owner">владелец окна</param>
        public PlaylistNameWindow(Window owner)
        {
            this.Owner = owner;

            InitializeComponent();
        }


        /// <summary>
        /// Сохранить
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == null || textBox.Text.Trim() == "")
            {
                Ascon.Dialogs.Dialogs.WarningMessage("Не указано имя файла плейлиста", this);
                return;
            }

            NAME = textBox.Text.Trim();
            this.DialogResult = true;
        }
    }
}