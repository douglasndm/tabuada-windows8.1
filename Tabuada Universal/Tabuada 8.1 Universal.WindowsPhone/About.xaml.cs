using System;
using Tabuada.Class;
using Tabuada.Common;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Tabuada
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class About : Page
    {
        Comum comum = new Comum();

        private NavigationHelper navigationHelper;

        public About()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void AppBarBtnEmail_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "nucleodosdownloads@outlook.com"
            };

            //generate mail object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Tabuada";


            //add recipients to the mail object
            mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            //mail.CC.Add(sendTo);

            //open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void appBarButtonRateApp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            comum.ReviewApp();
        }
    }
}
