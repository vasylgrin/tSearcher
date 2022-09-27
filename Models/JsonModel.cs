using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;

namespace tSearcher.Models
{
    internal class JsonModel
    {
        public ObservableCollection<Token> GetTopToken(int amountOfTokens)
        {
            ObservableCollection<Token> Tokens = new();

            var jToken = GetRequest("https://api.coincap.io/v2/assets");

            for (int count = 0; count < amountOfTokens; count++)
            {
                Token tkn = new(Convert.ToInt32(jToken[count]["rank"]), jToken[count]["id"].ToString(), jToken[count]["symbol"].ToString(), Convert.ToDouble(jToken[count]["priceUsd"]));
                Tokens.Add(tkn);
            }

            return Tokens;
        }

        public Token GetTokenForSearch(string searchToken)
        {
            var jToken = GetRequest("https://api.coincap.io/v2/assets");
            Token token = new();

            GetRequest("https://api.coincap.io/v2/assets").Where(tkn => tkn["id"].ToString().ToUpper() == searchToken.ToUpper() || tkn["symbol"].ToString().ToUpper() == searchToken.ToUpper())
                .ToList()
                .ForEach(tkn =>
                {
                    token = new Token(Convert.ToInt32(tkn["rank"]), tkn["id"].ToString(), tkn["symbol"].ToString(), Convert.ToDouble(tkn["priceUsd"]));
                });

            return token;
        }

        public ObservableCollection<Market> GetMarkets(string currentToken)
        {
            ObservableCollection<Market> markets = new();

            GetRequest($"https://api.coincap.io/v2/assets/{currentToken}/markets")
                .Where(mkt => mkt["baseId"].ToString().ToUpper() == currentToken.ToUpper() || mkt["baseSymbol"].ToString().ToUpper() == currentToken.ToUpper())
                .ToList()
                .ForEach(mkt =>
                {
                    markets.Add(new Market(mkt["exchangeId"].ToString(), mkt["baseId"].ToString(), mkt["baseSymbol"].ToString(), Math.Round(Convert.ToDouble(mkt["priceUsd"]), 4), mkt["quoteId"].ToString(), mkt["quoteSymbol"].ToString()));
                });

            return markets;
        }

        public JToken GetRequest(string adress)
        {
            string Response = "";
            HttpWebRequest? request = WebRequest.Create(adress) as HttpWebRequest;
            request.Method = "GET";

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                var stream = response.GetResponseStream();
                if (stream != null) Response = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
            }

            var nameJson = JObject.Parse(Response);
            return nameJson["data"];
        }
    }
}
