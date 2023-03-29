using PlaylistMaker.Contexts;
using PlaylistMaker.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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

                    ParseStatus();   
                }
            }
        }


        protected BitmapImage statusImage = null;
        /// <summary>
        /// Пиктограмма статуса
        /// </summary>
        public BitmapImage StatusImage
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


        protected string statusText = string.Empty;
        /// <summary>
        /// Текстовое пояснение состояния
        /// </summary>
        public string StatusText
        {
            get => statusText;
            protected set
            {
                if(statusText != value)
                {
                    statusText = value;
                    OnPropertyChanged();
                }
            }
        }


        protected Visibility statusVisibility = Visibility.Collapsed;
        /// <summary>
        /// Видимость статуса
        /// </summary>
        public Visibility StatusVisibility
        {
            get => statusVisibility;
            protected set
            {
                if(statusVisibility != value)
                {
                    statusVisibility = value;
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


        /// <summary>
        /// Обработка статуса
        /// </summary>
        protected void ParseStatus()
        {
            StatusImage = ItemStatusFabrique.GetImage(status);
            StatusText = Enums.GetItemStatus_Text(status);
            StatusVisibility = status != ItemStatus.Exist ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}