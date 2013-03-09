using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace Kulman.WinRT.UI
{
    public static class DialogHelper
    {
        public static async Task ShowMessage(string text)
        {
            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }

        public static async Task ShowTwoOptionsDialog(string text, string leftButtonText, string rightButtonText, Action leftButtonAction, Action rightButtonAction)
        {

            var dialog = new MessageDialog(text);
            

            var yesCmd = new UICommand(leftButtonText, c =>
            {
                if (leftButtonAction != null) leftButtonAction.Invoke();
            });
            var noCmd = new UICommand(rightButtonText, c =>
            {
                if (rightButtonAction != null) rightButtonAction.Invoke();
            });

            dialog.Commands.Add(yesCmd);
            dialog.Commands.Add(noCmd);

            dialog.DefaultCommandIndex = 1;

            await dialog.ShowAsync();
        }

        public static async Task ShowLocalizedDialog(string resourceKey)
        {
            var rl = new ResourceLoader();
            string dialogText = rl.GetString(resourceKey);
            var dialog = new MessageDialog(dialogText);
            await dialog.ShowAsync();
        }
    }
}
