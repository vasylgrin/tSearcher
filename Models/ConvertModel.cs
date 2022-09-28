using LiveCharts;
using MyCryptoApp.Controller;
using MyCryptoApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace tSearcher.Models
{
    internal class ConvertModel
    {
        JsonModel jsonModel = new();
        CandleModels candleModels = new();

        public string PrintCandlesGraph(string firstToken, string secondToken, out SeriesCollection seriesCollection)
        {
            seriesCollection = new();
            if (CheckForNull(firstToken, secondToken, out string err, out Token first, out Token second))
            {
                seriesCollection = candleModels.PrintCandles(first.FullName.ToLower(), second.FullName.ToLower(), out string errorMessage);
                return errorMessage;
            }
            else
            {
                return default;
            }
        }

        public string TokenConvert(string firstToken, double? firstTokenValue, string secondToken, out bool isOK)
        {
            isOK = false;
            if(CheckForNull(firstToken, secondToken, out string errorMessage, out Token first, out Token second))
            {
                double? res = (first.Price - (first.Price / 100 * 5)) * firstTokenValue / second.Price;

                isOK = true;
                return Math.Round((double)res, 4).ToString();
            }
            else
            {
                return errorMessage;
            }

        }

        private bool CheckForNull(string firstToken, string secondToken, out string errorMessage, out Token first, out Token second)
        {
            errorMessage = string.Empty;
            first = jsonModel.GetTokenForSearch(firstToken);
            second = jsonModel.GetTokenForSearch(secondToken);

            if (first.FullName == null)
            {
                errorMessage = $"{firstToken ?? "First token"} is not found.";
                return false;
            }
            else if (second.FullName == null)
            {
                errorMessage = $"{secondToken ?? "Second token"} is not found.";
                return false;
            }
            
            return true;
        }
    }
}
