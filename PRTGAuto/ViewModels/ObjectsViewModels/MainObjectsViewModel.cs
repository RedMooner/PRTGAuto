using Prism.Commands;
using Prism.Mvvm;
using PRTGAuto.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRTGAuto.ViewModels.ObjectsViewModels
{
    public class MainObjectsViewModel : BindableBase
    {
        public DelegateCommand<object> ShowSearchView { get; }
        public MainObjectsViewModel()
        {
            ShowSearchView = new DelegateCommand<object>(i =>
            {
                MainView.Instance.frame.Navigate(new Views.ObjectViews.SearchObjectsView());
            });
        }
    }
}
