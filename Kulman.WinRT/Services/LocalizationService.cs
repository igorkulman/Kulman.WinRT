using Windows.ApplicationModel.Resources;

namespace Kulman.WinRT.Services
{
    /// <summary>
    /// Localization service using the standard ResourceLoader
    /// </summary>
    public class LocalizationService : ILocalizationService
    {
        /// <summary>
        /// Gets a localized string
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Localized string</returns>
        public string Translate(string key)
        {
            var rl = new ResourceLoader();
            return rl.GetString(key);
        }
    }
}
