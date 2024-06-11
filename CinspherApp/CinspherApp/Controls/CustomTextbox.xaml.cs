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
    public partial class CustomTextbox : ContentView
    {
        public CustomTextbox()
        {
            InitializeComponent();
        }

        public string Text
        {
            get
            {
                return txt.Text;
            }
            set
            {
                txt.Text = value;
            }
        }

        public string Placeholder
        {
            get
            {
                return txt.Placeholder;
            }
            set
            {
                txt.Placeholder = value;
            }
        }

        public bool IsPassword
        {
            get
            {
                return txt.IsPassword;
            }
            set
            {
                txt.IsPassword = value;
            }
        }
        public CornerRadius CornerRadius
        {
            get
            {
                return pc.CornerRadius;
            }
            set
            {
                pc.CornerRadius = value;
            }
        }
    }
}