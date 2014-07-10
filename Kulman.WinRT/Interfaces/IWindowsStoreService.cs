using System.Threading.Tasks;
using Kulman.WinRT.Data;
using Kulman.WinRT.Services;

namespace Kulman.WinRT.Interfaces
{
    /// <summary>
    /// Interface for Windows Store service
    /// </summary>
    public interface IWindowsStoreService
    {
        bool IsPurchased(string productId);
        Task<PurchaseResponse> BuyAndGetReceipt(string productId);
        Task<string> GetPrice(string productId);
        Task<string> GetReceiptOrReceiptId(string productId);
    }
}
