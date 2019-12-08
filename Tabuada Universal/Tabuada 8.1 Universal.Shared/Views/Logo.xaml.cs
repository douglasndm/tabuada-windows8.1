using System;
using Tabuada.Class;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Tabuada.Views
{
    public sealed partial class Logo : UserControl
    {
        public Logo()
        {
            this.InitializeComponent();
        }

        private void AppLogo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AnimLogo.Begin();
        }

        /*

                #if WINDOWS_PHONE_APP
                
                #endif

                #if WINDOWS_APP
                #endif*/  
    }
}
