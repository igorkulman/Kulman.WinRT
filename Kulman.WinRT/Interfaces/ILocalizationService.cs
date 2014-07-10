namespace Kulman.WinRT.Interfaces
{
    /// <summary>
    /// Interface for application localization service
    /// </summary>
    public interface ILocalizationService
    {
        string Translate(string key);
    }
}
