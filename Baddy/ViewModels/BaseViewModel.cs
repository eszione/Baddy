using Baddy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Baddy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IAppContext _appContext;
        protected readonly INavigationService _navigationService;
        protected readonly IStorageService _storageService;

        public BaseViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService)
        {
            _appContext = appContext;
            _navigationService = navigationService;
            _storageService = storageService;
        }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private string title = string.Empty;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string error = string.Empty;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
