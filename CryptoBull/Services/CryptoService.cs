using CryptoBull.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;

namespace CryptoBull.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly Dictionary<string, Cryptocurrency> _cryptoDictionary;
        private readonly string _coinGeckoKey;

        public CryptoService(IConfiguration configuration, IHttpClientFactory httpClientFactory, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _httpClient = httpClient;
            _cryptoDictionary = new Dictionary<string, Cryptocurrency>();
            _coinGeckoKey = _configuration["AppSettings:CoinGeckoKey"];
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_coinGeckoKey}");
        }

        public async Task<Dictionary<string, Cryptocurrency>> GetCryptoData()
        {
            string apiUrl = "https://api.coincap.io/v2/assets?limit=20";
            var response = await _httpClient.GetAsync(apiUrl);
            var jsonObject = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());
            var dataArray = jsonObject.data;


            foreach (var item in dataArray)
            {

                Cryptocurrency cryptocurrency = new Cryptocurrency
                {
                    id = item.id,
                    rank = item.rank,
                    symbol = item.symbol,
                    name = item.name,
                    supply = item.supply,
                    maxSupply = item.maxSupply == null ? "Infinite" : item.maxSupply,
                    marketCapUsd = item.marketCapUsd,
                    volumeUsd24Hr = item.volumeUsd24Hr != null ? Convert.ToDouble(item.volumeUsd24Hr) : 0.0,
                    priceUsd = item.priceUsd != null ? Convert.ToDecimal(item.priceUsd) : 0.0m,
                    changePercent24Hr = item.changePercent24Hr != null ? Convert.ToDouble(item.changePercent24Hr) : 0.0,
                    vwap24Hr = item.vwap24Hr == null ? "-" : item.vwap24Hr,
                    explorer = item.explorer,
                    //imageUrl = mobulaUrl
                };

                _cryptoDictionary[cryptocurrency.id] = cryptocurrency;
            }

            return _cryptoDictionary;
        }

        public async Task<Cryptocurrency> GetCryptocurrency(string id)
        {
            if (_cryptoDictionary.Count == 0)
            {
                await GetCryptoData();
            }
            Cryptocurrency cryptoObject = _cryptoDictionary.Values.FirstOrDefault(crypto => crypto.id == id);
            return cryptoObject;
        }
        public async Task<List<Cryptocurrency>> GetTopMovers()
        {
            if (_cryptoDictionary.Count == 0)
            {
                await GetCryptoData();
            }
            var sortedCryptoList = _cryptoDictionary.Values.OrderByDescending(crypto => crypto.changePercent24Hr).ToList();

            var topThreeCryptocurrencies = sortedCryptoList.Take(3).ToList();

            return topThreeCryptocurrencies;
        }
    }
}
