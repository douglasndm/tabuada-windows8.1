using System;
using System.Diagnostics;
using Tabuada.Class;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Tabuada
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        AppSettings settings = new AppSettings();
        SpeechRecognizer Speech = new SpeechRecognizer();

        OrdemDosResultados ordem = OrdemDosResultados.Asc;
        private string SectionInView;
        double NumTabuar;
        short NumAteOnde;

        public MainPage()
        {
            this.InitializeComponent();

            GridResultados.Visibility = Visibility.Collapsed;

            AnimInicio.Begin();

            AppSettings settings = new AppSettings();

            if (settings.GetIntSetting("OpenTimes") == 15)
            {
                Comum comum = new Comum();
                comum.AskReview();
            }

            VerificarRemocaoAds();
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
                    NumTabuar = Convert.ToDouble(tb_num_tabuar.Text);
                    NumAteOnde = Convert.ToInt16(tb_num_ate_onde.Text);

                    if(NumAteOnde <= 0)
                    {
                        throw new ArgumentException();
                    }

                    // VERIFICA SE O USUÁRIO QUIS CONTAR QUANTAS VEZES FEZ CALCULOS E ADD MAIS 1
                    if (settings.GetBoolSettings("UpdateCount") == true)
                    {
                        settings.UpdateIntSettings("NumCount", settings.GetIntSetting("NumCount") + 1);
                    }


                    Calcular(ordem);

                    GridResultados.Visibility = Visibility.Visible;

                    appBarButtonInverter.Visibility = Visibility.Visible;
                    appBarButtonRead.Visibility = Visibility.Visible;

                    Speech.StopSpeech();
                    RemoverBotaoPausarEAddPlay();
                }
                catch(ArgumentException exception)
                {
                    var dialog = new MessageDialog("O número de quantas vezes tabuar não pode ser 0 ou negativo.");
                    await dialog.ShowAsync();
                }
                catch(Exception exception)
                {
                    var dialog = new MessageDialog("Algo realmente deu muito errado :(\nVocê digitou os dados corretamente?");
                    await dialog.ShowAsync();
                }
            }
        }

        private void Calcular(OrdemDosResultados ordem)
        {
            Results results = new Results();

            HubSectionSomando.DataContext = results.TabuadaSomando(NumTabuar, NumAteOnde, ordem);
            HubSectionSubtraindo.DataContext = results.TabuadaSubtraindo(NumTabuar, NumAteOnde, ordem);
            HubSectionMultiplicando.DataContext = results.TabuadaMultiplicando(NumTabuar, NumAteOnde, ordem);
            HubSectionDividindo.DataContext = results.TabuadaDividindo(NumTabuar, NumAteOnde, ordem);
            HubSectionResto.DataContext = results.TabuadaResto(NumTabuar, NumAteOnde, ordem);
            HubSectionRaiz.DataContext = results.TabuadaRaiz(NumTabuar, NumAteOnde, ordem);
        }

        private void appBarButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(about));
        }


        // VERIFICA A REMOÇÃO DOS ANÚNCIOS
        public void VerificarRemocaoAds()
        {
            RemoveAds removeAds = new RemoveAds();

            if(removeAds.VerificarCompra() == true)
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

        private void appBarButtonInverter_Click(object sender, RoutedEventArgs e)
        {
            Speech.StopSpeech();
            RemoverBotaoPausarEAddPlay();

            if (ordem == OrdemDosResultados.Asc)
            {
                ordem = OrdemDosResultados.Desc;
            }
            else
            {
                ordem = OrdemDosResultados.Asc;
            }

            Calcular(ordem);
        }

        private async void appBarButtonRead_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SectionInView))
            {
                var dialog = new MessageDialog("Clique no titulo de um dos resultados para ler");
                await dialog.ShowAsync();
            }
            else
            {
                RemoverBotaoLerEAddPausar();

                if (SectionInView == "somando")
                {
                    Speech.SpeechSomando(NumTabuar, NumAteOnde, ordem);
                }
                else if (SectionInView == "subtraindo")
                {
                    Speech.SpeechSubtraindo(NumTabuar, NumAteOnde, ordem);
                }
                else if (SectionInView == "multiplicando")
                {
                    Speech.SpeechMultiplicando(NumTabuar, NumAteOnde, ordem);
                }
                else if (SectionInView == "dividindo")
                {
                    Speech.SpeechDividindo(NumTabuar, NumAteOnde, ordem);
                }
                else if (SectionInView == "resto")
                {
                    Speech.SpeechResto(NumTabuar, NumAteOnde, ordem);
                }
                else if(SectionInView == "raiz")
                {
                    Speech.SpeechRaiz(NumTabuar, NumAteOnde, ordem);
                }
            } 

        }



        public void RemoverBotaoLerEAddPausar()
        {
            appBarButtonRead.Visibility = Visibility.Collapsed;
            appBarButtonPause.Visibility = Visibility.Visible;
        }
        public void RemoverBotaoPausarEAddPlay()
        {
            appBarButtonPause.Visibility = Visibility.Collapsed;
            appBarButtonRead.Visibility = Visibility.Visible;
        }

        private void appBarButtonPause_Click(object sender, RoutedEventArgs e)
        {
            Speech.StopSpeech();
            RemoverBotaoPausarEAddPlay();
        }


        #region
        private void HubSectionSomando_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "somando";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220,220,220,220));
            HubSectionSomando.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSubtraindo.Background = DefaultColor;
            HubSectionMultiplicando.Background = DefaultColor;
            HubSectionDividindo.Background = DefaultColor;
            HubSectionResto.Background = DefaultColor;
            HubSectionRaiz.Background = DefaultColor;
        }

        private void HubSectionSubtraindo_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "subtraindo";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220, 220, 220, 220));
            HubSectionSubtraindo.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSomando.Background = DefaultColor;
            HubSectionMultiplicando.Background = DefaultColor;
            HubSectionDividindo.Background = DefaultColor;
            HubSectionResto.Background = DefaultColor;
            HubSectionRaiz.Background = DefaultColor;
        }

        private void HubSectionMultiplicando_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "multiplicando";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220, 220, 220, 220));
            HubSectionMultiplicando.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSomando.Background = DefaultColor;
            HubSectionSubtraindo.Background = DefaultColor;
            HubSectionDividindo.Background = DefaultColor;
            HubSectionResto.Background = DefaultColor;
            HubSectionRaiz.Background = DefaultColor;
        }

        private void HubSectionDividindo_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "dividindo";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220, 220, 220, 220));
            HubSectionDividindo.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSomando.Background = DefaultColor;
            HubSectionSubtraindo.Background = DefaultColor;
            HubSectionMultiplicando.Background = DefaultColor;
            HubSectionResto.Background = DefaultColor;
            HubSectionRaiz.Background = DefaultColor;
        }

        private void HubSectionResto_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "resto";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220, 220, 220, 220));
            HubSectionResto.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSomando.Background = DefaultColor;
            HubSectionSubtraindo.Background = DefaultColor;
            HubSectionDividindo.Background = DefaultColor;
            HubSectionMultiplicando.Background = DefaultColor;
            HubSectionRaiz.Background = DefaultColor;
        }

        private void HubSectionRaiz_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            SectionInView = "raiz";
            SolidColorBrush color = new SolidColorBrush(Color.FromArgb(220, 220, 220, 220));
            HubSectionRaiz.Background = color;

            SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            HubSectionSomando.Background = DefaultColor;
            HubSectionSubtraindo.Background = DefaultColor;
            HubSectionDividindo.Background = DefaultColor;
            HubSectionMultiplicando.Background = DefaultColor;
            HubSectionResto.Background = DefaultColor;
        }

        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AnimInicio.Begin();
        }
    }
}