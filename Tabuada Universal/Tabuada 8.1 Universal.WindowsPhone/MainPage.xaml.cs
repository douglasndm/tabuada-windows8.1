using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Tabuada.Class;
using Windows.UI;

namespace Tabuada
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppSettings settings = new AppSettings();

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            StatusBar();

            AnimInicio.Begin();
            

            VerificarRemocaoAds();

            if (settings.GetIntSetting("OpenTimes") == 15)
            {
                Comum comum = new Comum();
                comum.AskReview();
            }
        }

        public void StatusBar()
        {
            var StatusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();

            StatusBar.BackgroundColor = Colors.DeepSkyBlue;
            StatusBar.BackgroundOpacity = 1;
            ///await StatusBar.HideAsync();
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (tb_num_tabuar.Text == "" || tb_num_ate_onde.Text == "")
            {
                var dialog = new MessageDialog("Digite os números para calcular");
                await dialog.ShowAsync();
            }
            else
            {
                try
                {
                    double NumTabuar = Convert.ToDouble(tb_num_tabuar.Text);
                    int NumAteOnde = Convert.ToInt32(tb_num_ate_onde.Text);

                    if (NumAteOnde <= 0)
                    {
                        throw new ArgumentException();
                    }

                    double[] parametros = new double[2];

                    parametros[0] = NumTabuar;
                    parametros[1] = NumAteOnde;

                    /*
                    string[,] parametros = new string[1,1];

                    parametros[0,0] = NumTabuar.ToString();
                    parametros[0,1] = NumAteOnde.ToString();*/

                    this.Frame.Navigate(typeof(results), parametros);
                }
                catch (ArgumentException exception)
                {
                    var dialog = new MessageDialog("O número de quantas vezes tabuar não pode ser 0 ou negativo.");
                    await dialog.ShowAsync();
                }
                catch (Exception exception)
                {
                    var dialog = new MessageDialog("Algo realmente deu muito errado :(\nVocê digitou os dados corretamente?\n\n Error:" + exception.Message);
                    await dialog.ShowAsync();
                }
            }
        }

        private void appBarButtonSobre_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(About));
        }

        // VERIFICA A REMOÇÃO DOS ANÚNCIOS
        public void VerificarRemocaoAds()
        {
            AppSettings settings = new AppSettings();

            if(settings.GetBoolSettings("RemoveAds") == true)
            {
                GridAd.Visibility = Visibility.Collapsed;
            }
        }

        private void AppBarButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(settings));
        }

        private void appBarButtonAprender_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(aprender));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AnimInicio.Begin(); 
        }
    }
}
