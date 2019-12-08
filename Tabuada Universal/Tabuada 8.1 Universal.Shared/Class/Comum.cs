using System;
using Windows.System;
using Windows.ApplicationModel.Store;
using Windows.UI.Popups;

namespace Tabuada.Class
{
    class Comum
    {
        public async void AskReview()
        {
            var dialog = new MessageDialog("Parece que você usa esse aplicativo com frequência, estou me esforçando para fazer ele cada vez melhor, poderia fazer um comentário sobre ele?");

            dialog.Commands.Add(new UICommand("Sim"));
            dialog.Commands.Add(new UICommand("Não"));

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (result.Label.Equals("Sim"))
            {
                ReviewApp();
            }
        }

        public async void ReviewApp()
        {
            #if WINDOWS_PHONE_APP
                await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
            #endif
            #if WINDOWS_APP
                await Launcher.LaunchUriAsync(new Uri("ms-windows-store://review/?AppId=" + CurrentApp.AppId));
            #endif

        }




        public void CreateTile(string text)
        {
        }
    }
}
