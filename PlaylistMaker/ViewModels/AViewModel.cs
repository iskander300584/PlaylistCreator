using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;


namespace PlaylistMaker.ViewModels
{
    /// <summary>
    /// Визуальное представление элемента
    /// </summary>
    public abstract class AViewModel : INotifyPropertyChanged
    {
        protected ItemStatus status = ItemStatus.Exist;
        /// <summary>
        /// Статус
        /// </summary>
        public ItemStatus Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged();

                    StatusImage = ItemStatusFabrique.GetImage(status);
                }
            }
        }


        protected System.Windows.Media.ImageSource statusImage = null;
        /// <summary>
        /// Пиктограмма статуса
        /// </summary>
        public System.Windows.Media.ImageSource StatusImage
        {
            get => statusImage;
            protected set
            {
                if (statusImage != value)
                {
                    statusImage = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Изменение свойства
        /// </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        /// <summary>
        /// Получение статуса
        /// </summary>
        /// <param name="parameter">параметр</param>
        protected abstract void GetStatus(string parameter = "");
    }
}