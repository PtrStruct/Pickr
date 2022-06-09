using Pickr.Core;
using Pickr.MVVM.Model;
using System;
using System.Windows;

namespace Pickr.MVVM.ViewModel
{
    class MainViewModel
    {

        /* Commands */
        public RelayCommand DragMove { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }    
            set { _currentView = value; }
        }


        public MainViewModel()
        {
            CurrentView = new ColorPickerViewModel();
            DragMove = new RelayCommand(o => Application.Current.MainWindow.DragMove(), o => true);
        }
    }
}
