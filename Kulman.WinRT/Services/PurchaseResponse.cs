namespace Kulman.WinRT.Services
{
    public enum PurchaseResult
    {
        Ok,
        Cancel,
        Error
    }

    public class PurchaseResponse
    {
        public PurchaseResult Result { get; set; }
        public string Receipt { get; set; }
    }
}
