using CinspherApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void TapSignUp_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Pages.SignUpPage());
        }

        private async void CustomButton_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var response = await ApiServices.Login(txtUserName.Text, txtPassword.Text);
            Preferences.Set("password", txtPassword.Text);
            Preferences.Set("userName", txtUserName.Text);
            if (response)
            {
                //Application.Current.MainPage = new AppShell();
                activityIndicator.IsRunning = true;
                Application.Current.MainPage = new NavigationPage(new MainPage());

            }
            else
            {
                activityIndicator.IsRunning = false;
                await DisplayAlert("تنبيه", "خطأ في اسم المستخدم و كلمة السر", "OK");
            }
        }
    }
}