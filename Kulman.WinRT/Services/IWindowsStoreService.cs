using System.Threading.Tasks;

namespace Kulman.WinRT.Services
{
    public interface IWindowsStoreService
    {
        bool IsPurchased(string productId);
        Task<PurchaseResponse> BuyAndGetReceipt(string productId);
        Task<string> GetPrice(string productId);
        Task<string> GetReceiptOrReceiptId(string productId);
    }
}
