using Windows.ApplicationModel.Resources;

namespace Kulman.WinRT.Services
{
    public class LocalizationService: ILocalizationService
    {
        public string Translate(string key)
        {
            var rl = new ResourceLoader();
            return rl.GetString(key);
        }
    }
}
