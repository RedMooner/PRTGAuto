using Prism.Commands;
using Prism.Mvvm;
using PRTGAuto.Views;
using PRTGAuto.Views.ObjectViews;
using PRTGAuto.Views.SensorsViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTGAuto.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private string _title = "Главная";
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        public DelegateCommand<object> ShowObjectView { get; }
        public DelegateCommand<object> ShowSensorsView { get; }

        public MainViewModel()
        {
            ShowObjectView = new DelegateCommand<object>(i =>
            {
                MainView.Instance.frame.Navigate(new MainObjectView());
                Title = "Устройства";
            });
            ShowSensorsView = new DelegateCommand<object>(i =>
            {
                MainView.Instance.frame.Navigate(new MainSensorsView());
                Title = "Сенсоры";
            });
        }
    }
}
