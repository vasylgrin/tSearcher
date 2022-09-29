using LiveCharts;
using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace tSearcher.Models
{
    internal class ConvertModel
    {
        JsonModel jsonModel = new();
        CandleModels candleModels = new();

        public SeriesCollection SeriesCollection { get; set; } = new();
        public string? ErrorMessage { get; set; }

        List<Token> Tokens = new();

        public async Task PrintCandlesGraph(string firstToken, string secondToken)
        {
            SeriesCollection = candleModels.PrintCandles(Tokens[0].FullName.ToLower(), Tokens[1].FullName.ToLower(), out string errorMessage);
            ErrorMessage = errorMessage;
            Tokens.Clear();
        }

        public async Task<string> TokenConvert(string firstToken, double? firstTokenValue, string secondToken)
        {
            if(await Gettoken(firstToken) || await Gettoken(secondToken))
                return null;

            double? res = (Tokens[0].Price - (Tokens[0].Price / 100 * 5)) * firstTokenValue / Tokens[1].Price;

            return Math.Round((double)res, 4).ToString();
        }

        private async Task<bool> Gettoken(string token)
        {
            await jsonModel.GetTokenForSearch(token);
            
            if(jsonModel.Token != null)
            {
                Tokens.Add(jsonModel.Token);
                jsonModel.Token = null;
                return false;
            }
            else
            {
                ErrorMessage = $"{token} can not be empty";
                jsonModel.Token = null;
                return true;
            }       
        }
    }
}
