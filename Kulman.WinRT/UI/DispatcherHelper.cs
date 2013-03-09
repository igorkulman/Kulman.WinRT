using System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Kulman.WinRT.UI
{
    public static class DispatcherHelper
    {
        public static CoreDispatcher UIDispatcher { get; private set; }

        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (UIDispatcher.HasThreadAccess)
                action();
            else UIDispatcher.RunAsync(CoreDispatcherPriority.Normal,
                                       () => action());
        }

        static DispatcherHelper()
        {
            if (UIDispatcher != null)
                return;
            else UIDispatcher = Window.Current.Dispatcher;
        }
    }
}
