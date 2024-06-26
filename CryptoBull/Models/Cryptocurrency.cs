﻿namespace CryptoBull.Models
{
    public class Cryptocurrency
    {
        public string id { get; set; }
        public int rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string supply { get; set; }
        public string maxSupply { get; set; }
        public string marketCapUsd { get; set; }
        public double volumeUsd24Hr { get; set; }
        public decimal priceUsd { get; set; }
        public double changePercent24Hr { get; set; }
        public string vwap24Hr { get; set; }
        public string explorer { get; set; }

        public string imageUrl { get; set; }
    }
}
