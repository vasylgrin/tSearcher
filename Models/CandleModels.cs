using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using tSearcher.Models;

namespace MyCryptoApp.Controller
{
    internal class CandleModels
    {
        #region Property

        JsonModel jsonModel = new();
        readonly ChartValues<ObservableValue> val0 = new();
        readonly ChartValues<ObservableValue> val1 = new();
        readonly ChartValues<OhlcPoint> val2 = new();

        #endregion

        /// <summary>
        /// Output of candles on the chart.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        /// <param name="seriesCollection">The result of the graph.</param>
        public SeriesCollection PrintCandles(string baseId, string? quoteId, out string errorMessage)
        {
            errorMessage = "";
            
            if (string.IsNullOrWhiteSpace(baseId) || string.IsNullOrWhiteSpace(quoteId))
            {
                return default;
            }

            if(AddCandle(baseId, quoteId, out errorMessage))
            {
                return new SeriesCollection();
            }

            SeriesCollection _seriesCollection = new SeriesCollection
                {
                new LineSeries
                {
                    Title = "",
                    Stroke = Brushes.Aqua,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 10,
                    PointGeometry = DefaultGeometries.Square,
                    Values = val0
                },
                new LineSeries
                {
                    Title = "",
                    Stroke = Brushes.Red,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 5,
                    Values = val1
                },
                new OhlcSeries
                {
                    Values = val2
                }
                };

            return _seriesCollection;
        }

        /// <summary>
        /// We choose the number of candles and add them to the collection.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        private bool AddCandle(string? baseId, string? quoteId, out string errorMessage)
        {
            errorMessage = "";
            var candles = GetAllCandles(baseId, quoteId);

            if (candles.Count == 0)
            {
                errorMessage = "Candle not found.";
                return true;
            }

            EMA ind0 = new(20);
            EMA ind1 = new(10);

            for (int i = 0; i < 30; i++)
            {
                ind0.Add(candles[i].Close);
                ind1.Add(candles[i].Close);

                val0.Add(new ObservableValue(ind0.Value[0]));
                val1.Add(new ObservableValue(ind1.Value[0]));

                val2.Add(new OhlcPoint(candles[i].Open, candles[i].High, candles[i].Low, candles[i].Close));
            }
            return false;
        }

        /// <summary>
        /// Getting all the candles for two tokens.
        /// </summary>
        /// <param name="baseId">Name of the main token.</param>
        /// <param name="quoteId">Сurrency token.</param>
        private List<Candles> GetAllCandles(string? baseId, string? quoteId)
        {
            List<Candles> candles = new();

            jsonModel.GetRequest($"https://api.coincap.io/v2/candles?exchange=poloniex&interval=h8&baseId={baseId}&quoteId={quoteId}")
                .ToList().ForEach(cnd =>
                {
                    Candles candle = new(Convert.ToDouble(cnd["open"]), Convert.ToDouble(cnd["high"]), Convert.ToDouble(cnd["low"]), Convert.ToDouble(cnd["close"]));
                    candles.Add(candle);
                });

            return candles;
        }
    }

}