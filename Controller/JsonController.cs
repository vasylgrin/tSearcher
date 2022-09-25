using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using tSearcher.Models;

namespace tSearcher.Controller
{
    internal class JsonController
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
            #region Variables

            Token variable = new();
            List<Token> Tokens = new();

            #endregion

            var jToken = GetRequest("https://api.coincap.io/v2/assets");

            foreach (var item in jToken)
            {
                Token t = new(Convert.ToInt32(item["rank"]), item["id"].ToString(), item["symbol"].ToString(), Convert.ToDouble(item["priceUsd"]));
                Tokens.Add(t);
            }

            foreach (var tokens in Tokens)
            {
                if (tokens.Symbol.Contains(searchToken.ToUpper()))
                {
                    return tokens;
                }

                if (tokens.FullName.ToUpper().Contains(searchToken.ToUpper()))
                {
                    return tokens;
                }
            }
            return null;
        }

        public ObservableCollection<Market> GetMarkets(string currentToken)
        {
            ObservableCollection<Market> marketList = new();
            ObservableCollection<Market> markets = new();
            var jToken = GetRequest($"https://api.coincap.io/v2/assets/{currentToken}/markets");

            foreach (var item in jToken)
            {
                marketList.Add(new Market(
                    item["exchangeId"].ToString(),
                    item["baseId"].ToString(),
                    item["baseSymbol"].ToString(),
                    Math.Round(Convert.ToDouble(item["priceUsd"]), 4),
                    item["quoteId"].ToString(),
                    item["quoteSymbol"].ToString()
                    ));
            }

            foreach (var item in marketList)
            {
                if (item.TokenSymbol.ToUpper() == currentToken.ToUpper())
                {
                    markets.Add(item);
                }
                if (item.TokenName.ToUpper() == currentToken.ToUpper())
                {
                    markets.Add(item);
                }
            }

            return markets;
        }

        private JToken GetRequest(string adress)
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
