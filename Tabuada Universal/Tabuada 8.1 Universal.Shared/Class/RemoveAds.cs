using System;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;

namespace Tabuada.Class
{
    class RemoveAds
    {
        private const string IAPName = "RemoveAds";

        public bool VerificarCompra()
        {
            var licenseInformation = CurrentApp.LicenseInformation;

            if (licenseInformation.ProductLicenses[IAPName].IsActive)
            {
                return true;
                // the customer can access this feature
            }
            else
            {
                return false;
                // the customer can't access this feature
            }
        }



        public async void ComprarRemoverAnuncios()
        {
            AppSettings settings = new AppSettings();

            LicenseInformation licenseInformation = CurrentApp.LicenseInformation;

            if (!licenseInformation.ProductLicenses[IAPName].IsActive)
            {
                try
                {
                    // TENTANTIVA DE COMPRA
                    await CurrentApp.RequestProductPurchaseAsync(IAPName);

                    if (licenseInformation.ProductLicenses[IAPName].IsActive)
                    {
                        // GRAVA A COMPRA DA REMOÇÃO DOS ANÚNCIOS NAS CONFIGURAÇÕES DO APP
                        settings.UpdateBoolSettings("RemoveAds", true);

                        var dialog = new MessageDialog("Você removeu pernamenteme os anúncios");
                        await dialog.ShowAsync();
                    }
                    else
                    {
                        settings.UpdateBoolSettings("RemoveAds", false);

                        var dialog = new MessageDialog("Compra não concluida");
                        await dialog.ShowAsync();
                    }
                }
                catch
                {
                    settings.UpdateBoolSettings("RemoveAds", false);

                    var dialog = new MessageDialog("Algo deu errado durante a compra do pacote de remoção dos anúncios");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                // GARANTE A GRAVAÇÃO DA COMPRA DA REMOÇÃO DOS ANÚNCIOS NAS CONFIGURAÇÕES DO APP
                settings.UpdateBoolSettings("RemoveAds", true);

                var dialog = new MessageDialog("Você já comprou o pacote de remoção de anúncios");
                await dialog.ShowAsync();
            }
        }
    }
}
