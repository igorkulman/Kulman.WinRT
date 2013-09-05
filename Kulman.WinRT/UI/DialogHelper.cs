using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace Kulman.WinRT.UI
{
    /// <summary>
    /// Helper class for showing dialogs
    /// </summary>
    public static class DialogHelper
    {
        /// <summary>
        /// Shows a dialog with given text
        /// </summary>
        /// <param name="text">Text to be shown in dialog</param>
        /// <returns>Task</returns>
        public static async Task ShowMessage(string text)
        {
            var dialog = new MessageDialog(text);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Shows a dialog with given text and two buttons
        /// </summary>
        /// <param name="text">Text to be shown in dialog</param>
        /// <param name="leftButtonText">Text on the left button</param>
        /// <param name="rightButtonText">Text on the right button</param>
        /// <param name="leftButtonAction">Left button action</param>
        /// <param name="rightButtonAction">Right button action</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Shows a dialog with localized message using ResourceLoader
        /// </summary>
        /// <param name="resourceKey">Key</param>
        /// <returns>Task</returns>
        public static async Task ShowLocalizedDialog(string resourceKey)
        {
            var rl = new ResourceLoader();
            string dialogText = rl.GetString(resourceKey);
            var dialog = new MessageDialog(dialogText);
            await dialog.ShowAsync();
        }
    }
}
