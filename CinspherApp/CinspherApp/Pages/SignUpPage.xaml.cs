using CinspherApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            this.IsBusy = false;
        }
        private async void CustomButton_Clicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var newuser = new Models.AccountRegister()
            {
                UserName = txt_UserName.Text,
                Password = txt_PassWord.Text,
                FullName = txtFullName.Text,
                Email = txt_Email.Text,
                Phone=txt_PhoneNumber.Text,




            };
            var res = await ApiServices.Register(newuser.Email, newuser.FullName, newuser.Password, newuser.UserName,newuser.Phone);
            if (res)
            {
                this.IsBusy = false; ;
                await DisplayAlert("", "Successfully Registered", "Continue");
                await Navigation.PushModalAsync(new Pages.Login());


            }
            else
            {
                this.IsBusy = false;
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
                //activityIndicator.IsRunning = true;
            }
        }
    }
}