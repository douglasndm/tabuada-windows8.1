using Tabuada.Class;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tabuada.Views
{
    public sealed partial class Settings : UserControl
    {
        AppSettings settings = new AppSettings();

        public Settings()
        {
            this.InitializeComponent();

            //toggleSwitchUpdateTile.IsOn = settings.GetBoolSettings("UpdateLiveTile");
            toggleSwitchCount.IsOn = settings.GetBoolSettings("UpdateCount");

            tb_cont.Text = "Cálculos feitos: " + settings.GetIntSetting("NumCount");

            if(settings.GetBoolSettings("UpdateCount") == false)
            {
                btnZerarContador.IsEnabled = false;
            }
        }

        

        private void BtnBuyRemoveAds_Click(object sender, RoutedEventArgs e)
        {
            RemoveAds removeads = new RemoveAds();
            removeads.ComprarRemoverAnuncios();
        }

        private void toggleSwitchUpdateTile_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
        }

        private void toggleSwitchUpdateTile_Toggled(object sender, RoutedEventArgs e)
        {
            //settings.UpdateBoolSettings("UpdateLiveTile", toggleSwitchUpdateTile.IsOn);
        }

        private void toggleSwitchCount_Toggled(object sender, RoutedEventArgs e)
        {
            if(toggleSwitchCount.IsOn == false)
            {
                settings.UpdateIntSettings("NumCount", 0);
                tb_cont.Text = "Cálculos feitos: " + settings.GetIntSetting("NumCount");
                btnZerarContador.IsEnabled = false;
            }
            else
            {
                btnZerarContador.IsEnabled = true;
            }
            settings.UpdateBoolSettings("UpdateCount", toggleSwitchCount.IsOn);
        }

        private void btnZerarContador_Click(object sender, RoutedEventArgs e)
        {
            settings.UpdateIntSettings("NumCount", 0);
            tb_cont.Text = "Cálculos feitos: " + settings.GetIntSetting("NumCount");
        }
    }
}
