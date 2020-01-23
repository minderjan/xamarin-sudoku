using System;
using System.ComponentModel;

namespace sudoku.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RegisterPropertyChangedHandler(PropertyChangedEventHandler handler) {
            this.PropertyChanged += handler;
        }

    }
}
