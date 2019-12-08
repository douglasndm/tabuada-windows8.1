using System;
using Tabuada.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Tabuada.Class;
using Windows.Media.SpeechSynthesis;
using Windows.Phone.UI.Input;
using Windows.UI;

namespace Tabuada
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class results : Page
    {
        AppSettings settings = new AppSettings();
        SpeechRecognizer Speech = new SpeechRecognizer();

        OrdemDosResultados ordem;

        double NumTabuar;
        short NumAteOnde;

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public results()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;


            var StatusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            StatusBar.BackgroundColor = Colors.White;
            StatusBar.BackgroundOpacity = 0;



            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            // VERIFICA SE O USUÁRIO QUIS CONTAR QUANTAS VEZES FEZ CALCULOS E ADD MAIS 1
            if (settings.GetBoolSettings("UpdateCount") == true)
            {
                settings.UpdateIntSettings("NumCount", settings.GetIntSetting("NumCount") + 1);
            }

            if(settings.GetBoolSettings("RemoveAds") == true)
            {           
            }
        }

        private void Calcular(double NumTabuar, short NumAteOnde, OrdemDosResultados ordem)
        {
            Results results = new Results();
            
            HubSectionSomando.DataContext = results.TabuadaSomando(NumTabuar, NumAteOnde, ordem);
            HubSectionSubtraindo.DataContext = results.TabuadaSubtraindo(NumTabuar, NumAteOnde, ordem);
            HubSectionMultiplicando.DataContext = results.TabuadaMultiplicando(NumTabuar, NumAteOnde, ordem);
            HubSectionDividindo.DataContext = results.TabuadaDividindo(NumTabuar, NumAteOnde, ordem);
            HubSectionResto.DataContext = results.TabuadaResto(NumTabuar, NumAteOnde, ordem);
            HubSectionRaiz.DataContext = results.TabuadaRaiz(NumTabuar, NumAteOnde, ordem);
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Speech.StopSpeech();
                e.Handled = true;
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            double[] parametros = e.NavigationParameter as double[];

            if(parametros[0] != 0 && parametros[1] != 0)
            {
                NumTabuar = Convert.ToDouble(parametros[0]);
                NumAteOnde = Convert.ToInt16(parametros[1]);

                ordem = OrdemDosResultados.Asc;
                
                Calcular(NumTabuar, NumAteOnde, ordem);                
            }
            else
            {
                // MENSAGEM DE ERRO CASO NÃO HAJA PARAMETROS
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
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

        private void AppBarButtonInverter_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
            
            Calcular(NumTabuar, NumAteOnde, ordem);
        }

        public void RemoverBotaoLerEAddPausar()
        {
            AppBarButtonRead.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AppBarButtonPause.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
        public void RemoverBotaoPausarEAddPlay()
        {
            AppBarButtonPause.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            AppBarButtonRead.Visibility = Windows.UI.Xaml.Visibility.Visible;
            /*
            AppBarButton AddPlayButton = new AppBarButton { Label = "Ler", Name = "AppBarButtonRead", Icon = new SymbolIcon(Symbol.Play) };
            AddPlayButton.Click += AppBarButtonRead_Click;
            (BottomAppBar as CommandBar).PrimaryCommands.Add(AddPlayButton);*/
        }

        private void AppBarButtonPause_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Speech.StopSpeech();
            RemoverBotaoPausarEAddPlay();
        }

        private void AppBarButtonRead_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RemoverBotaoLerEAddPausar();

            var section = ResultsHub.SectionsInView[0];
            var tag = section.Tag.ToString();

            if(tag == "somando")
            {
                Speech.SpeechSomando(NumTabuar, NumAteOnde, ordem);
            }
            else if(tag == "subtraindo")
            {
                Speech.SpeechSubtraindo(NumTabuar, NumAteOnde, ordem);
            }
            else if (tag == "multiplicando")
            {
                Speech.SpeechMultiplicando(NumTabuar, NumAteOnde, ordem);
            }
            else if (tag == "dividindo")
            {
                Speech.SpeechDividindo(NumTabuar, NumAteOnde, ordem);
            }
            else if (tag == "resto")
            {
                Speech.SpeechResto(NumTabuar, NumAteOnde, ordem);
            }
            else if(tag == "raiz")
            {
                Speech.SpeechRaiz(NumTabuar, NumAteOnde, ordem);
            }
        }

        private void ResultsHub_SectionsInViewChanged(object sender, SectionsInViewChangedEventArgs e)
        {
            Speech.StopSpeech();
            RemoverBotaoPausarEAddPlay();
        }
    }
}
