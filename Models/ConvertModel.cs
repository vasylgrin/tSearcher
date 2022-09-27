using LiveCharts;
using MyCryptoApp.Controller;
using System;
using System.Threading.Tasks;

namespace tSearcher.Models
{
    internal class ConvertModel
    {
        JsonModel jsonModel = new();
        CandleModels candleModels = new();

        public SeriesCollection PrintCandlesGraph(string firstToken, string secondToken)
        {
            var Token1 = jsonModel.GetTokenForSearch(firstToken);
            var Token2 = jsonModel.GetTokenForSearch(secondToken);

            return candleModels.PrintCandles(Token1.FullName.ToLower(), Token2.FullName.ToLower());
        }

        public string TokenConvert(string firstToken, double? firstTokenValue, string secondToken)
        {
            var first = jsonModel.GetTokenForSearch(firstToken);
            var second = jsonModel.GetTokenForSearch(secondToken);

            double? res = (first.Price - (first.Price / 100 * 5)) * firstTokenValue / second.Price;

            return Math.Round((double)res, 4).ToString();
        }
    }
}
