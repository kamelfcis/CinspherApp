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
    public partial class MyBooking : ContentPage
    {
        public MyBooking()
        {
            InitializeComponent();
            AnimateTitle();
        }

        private async void AnimateTitle()
        {
            await TitleLabel.FadeTo(1, 2000);
        }

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                await frame.ScaleTo(0.95, 100); // Scale down
                await frame.ScaleTo(1, 100);    // Scale back to original size
            }
        }
    }
}