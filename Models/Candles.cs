using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using tSearcher.Models;

namespace MyCryptoApp.Controller
{
    internal class CandlesController
    {
        #region Property

        JsonModel jsonModel = new();
        readonly List<Candles> candles = new();
        readonly ChartValues<ObservableValue> val0 = new();
        readonly ChartValues<ObservableValue> val1 = new();
        readonly ChartValues<OhlcPoint> val2 = new();

        #endregion

        protected bool PrintCandles(string? baseId, string? quoteId, out SeriesCollection seriesCollection)
        {
            SeriesCollection _seriesCollection = new();

            if (string.IsNullOrWhiteSpace(baseId) || string.IsNullOrWhiteSpace(quoteId))
            {
                seriesCollection = new SeriesCollection();
                return true;
            }

            if (AddCandle(baseId, quoteId))
            {
                seriesCollection = new SeriesCollection();
                return true;
            }

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _seriesCollection = new SeriesCollection
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
            });

            seriesCollection = _seriesCollection;
            return false;
        }

        private bool AddCandle(string? baseId, string? quoteId)
        {
            if (GetAllCandles(baseId, quoteId))
            {
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

        private bool GetAllCandles(string? baseId, string? quoteId)
        {
            var jToken = jsonModel.GetRequest($"https://api.coincap.io/v2/candles?exchange=poloniex&interval=h8&baseId={baseId}&quoteId={quoteId}").ToList();

            foreach (var item in jToken)
            {
                Candles candle = new(Convert.ToDouble(item["open"]), Convert.ToDouble(item["high"]), Convert.ToDouble(item["low"]), Convert.ToDouble(item["close"]));
                candles.Add(candle);
            }

            if (candles.Count == 0)
                return true;
            else
                return false;
        }
    }

}
