using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
            AnimationLogo();
        
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


        }
        public async void AnimationLogo()
        {
            double _scale = 0;
            _scale = imgLocation.Scale;
            await imgLocation.ScaleTo(0, 0);

            await lblDeliver.TranslateTo(0, 400, 0);


            await sl.TranslateTo(0, 400, 0);
            //await lblAddress.TranslateTo(0, 400, 0);


            await img.ScaleTo(0.5, 100);
            await img.TranslateTo(img.X, -200, 400, Easing.Linear);
            await img.TranslateTo(0, -80, 400, Easing.Linear);


            //img.IsVisible = false;
            // grid.IsVisible = true;
            uint speed = 400;
            var path1Anim = new Animation(_ => img.ScaleTo(0, speed));
            var path2Anim = new Animation(_ => imgLocation.ScaleTo(_scale, speed));
            var path3Anim = new Animation(_ => lblDeliver.TranslateTo(0, 0, speed));
            //var path4Anim = new Animation(_ => lblAddress.TranslateTo(0, 0, speed));
            var path5Anim = new Animation(_ => sl.TranslateTo(0, 0, speed));

            var masterAnimation = new Animation
            {
                { 0, 1, path1Anim },
                { 0, 1, path2Anim },
                { 0, 1, path3Anim },
                //{ 0, 1, path4Anim },
                { 0, 1, path5Anim },

            };
            masterAnimation.Commit(this, "MyAnim");

            //  await imgLocation.ScaleTo(_scale, 300);



        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        private async void btn_Go_Tapped(object sender, EventArgs e)
        {
            base.OnAppearing();

            var accessToken = Preferences.Get("accessToken", String.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                //MainPage = new AppShell();
                await Navigation.PushModalAsync(new Pages.Login());
                //Application.Current.MainPage = new NavigationPage(new Pages.SignUp());

            }
            else
            {
                //Application.Current.MainPage = new NavigationPage(new Pages.HomeTabbed());
                Application.Current.MainPage = new NavigationPage(new MainPage());
                //await Navigation.PushModalAsync();
                //MainPage = new AppShell();
                //Application.Current.MainPage = new AppShell();
            }

        }
    }
}