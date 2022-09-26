using System;

namespace tSearcher.Models
{
    internal class ConvertModel
    {
        JsonModel jsonModel = new();

        public string TokenConvert(string firstToken, double? firstTokenValue, string secondToken)
        {
            var first = jsonModel.GetTokenForSearch(firstToken);
            var second = jsonModel.GetTokenForSearch(secondToken);

            double? res = (first.Price - (first.Price / 100 * 5)) * firstTokenValue / second.Price;

            return Math.Round((double)res, 4).ToString();
        }
    }
}
