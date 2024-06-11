using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinspherApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomButton : ContentView
    {
        public event EventHandler Clicked;

        public CustomButton()
        {
            InitializeComponent();
            btn.Clicked += new EventHandler(OnClicked);
        }

        protected void OnClicked(object sender, EventArgs args)
        {
            if (Clicked != null)
            {
                this.Clicked?.Invoke(sender, args);
            }
        }

        public string Text
        {
            get
            {
                return btn.Text;
            }
            set
            {
                btn.Text = value;
            }
        }
    }
}