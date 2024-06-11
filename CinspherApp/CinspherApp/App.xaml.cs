using Plugin.SharedTransitions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Device.SetFlags(new[] { "Shapes_Experimental" });
            //MainPage = new NavigationPage(new MainPage());

            var accessToken = Preferences.Get("accessToken", String.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                //MainPage = new AppShell();
                Application.Current.MainPage = new NavigationPage(new Pages.LandingPage());
                //Application.Current.MainPage = new NavigationPage(new Pages.SignUp());

            }
            else
            {
                //Application.Current.MainPage = new NavigationPage(new Pages.HomeTabbed());
                Application.Current.MainPage = new SharedTransitionNavigationPage(new Pages.LandingPage());
                //await Navigation.PushModalAsync();
                //MainPage = new AppShell();
                //Application.Current.MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
