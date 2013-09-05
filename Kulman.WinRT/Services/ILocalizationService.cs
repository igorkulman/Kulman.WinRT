namespace Kulman.WinRT.Services
{
    /// <summary>
    /// Interface for application localization service
    /// </summary>
    public interface ILocalizationService
    {
        string Translate(string key);
    }
}
