using PlaylistMaker.Contexts;
using System.ComponentModel;
using System.Runtime.CompilerServices;


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