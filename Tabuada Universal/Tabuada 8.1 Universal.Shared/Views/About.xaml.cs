using System;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Tabuada.Views
{
    public sealed partial class About : UserControl
    {
        public About()
        {
            this.InitializeComponent();

            PackageVersion pv = Package.Current.Id.Version;

            Version version = new Version(Package.Current.Id.Version.Major,
                                          Package.Current.Id.Version.Minor,
                                          Package.Current.Id.Version.Revision,
                                          Package.Current.Id.Version.Build);

            AppVersion.Text = "Versão: " + version;


            #if WINDOWS_PHONE_APP
#endif

#if WINDOWS_APP
#endif
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }
    }
}
