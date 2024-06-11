using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CinspherApp.ViewModels
{
    public class CustomAlertViewModel : BindableObject
    {
        public ICommand CloseCommand { get; }

        public CustomAlertViewModel()
        {
            CloseCommand = new Command(Close);
        }

        private   void Close()
        {
           Application.Current.MainPage=new MainPage(); 
        }
    }
}
