using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace Kulman.WinRT.UI
{
    public static class DialogHelper
    {
        public static async Task ShowMessage(string text)
        {
            var dialog = new Windows.UI.Popups.MessageDialog(text);
            var result = await dialog.ShowAsync();
        }

        public static async Task ShowTwoOptionsDialog(string text, string leftButtonText, string rightButtonText, Action leftButtonAction, Action rightButtonAction)
        {

            MessageDialog dialog = new MessageDialog(text);
            UICommandInvokedHandler cmdHandler = new UICommandInvokedHandler(cmd =>
            {
                if (leftButtonAction != null) leftButtonAction.Invoke();
            });

            UICommand yesCmd = new UICommand(leftButtonText, c =>
            {
                if (leftButtonAction != null) leftButtonAction.Invoke();
            });
            UICommand noCmd = new UICommand(rightButtonText, c =>
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
            ResourceLoader rl = new ResourceLoader();
            string dialogText = rl.GetString(resourceKey);
            var dialog = new Windows.UI.Popups.MessageDialog(dialogText);
            var result = await dialog.ShowAsync();
            return;
        }
    }
}
