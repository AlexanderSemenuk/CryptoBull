using CryptoBull.Models;
namespace CryptoBull.Services
{
    public interface ICryptoService
    {
        Task<Dictionary<string, Cryptocurrency>> GetCryptoData();

        Task<Cryptocurrency> GetCryptocurrency(string id);

        Task<List<Cryptocurrency>> GetTopMovers();
    }
}
