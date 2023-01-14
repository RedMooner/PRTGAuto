using Prism.Commands;
using Prism.Mvvm;
using PRTGAuto.Models;
using PRTGAuto.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PRTGAuto.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private string _url = "https://mon.cirpk.permkrai.ru";
        private string _username = "ammotyrev";

        public string Url
        {
            get { return _url; }
            set
            {
                if(value == "")
                {
                    throw new ArgumentException("Пожалуйста введите ссылку!");
                }
                _url = value;
            }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public DelegateCommand<object> LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new DelegateCommand<object>(i =>
            {
                if (PRTGConnection.Connect(Url, Username, LoginView.Instance.passwordBox.Password))
                {
                    MainWindow.Instance.frame.Navigate(new MainView());
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль!");
                }
            });
        }
    }
}
