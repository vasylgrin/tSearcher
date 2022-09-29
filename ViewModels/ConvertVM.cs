using LiveCharts;
using MyCryptoApp.Controller;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    class ConvertVM : BaseVM
    {
        #region Property
        private string? _firstToken;
        private double? _firstValueToken = 1;
        private string? _secondToken;
        private string? _printConvertToken;
        private SeriesCollection _seriesCollection = new();

        public string? FirstToken { get { return _firstToken; } set { _firstToken = value; OnPropertyChanged(); } }
        public double? FirstValueToken { get { return _firstValueToken; } set { _firstValueToken = value; OnPropertyChanged(); } }
        public string? SecondToken { get { return _secondToken; } set { _secondToken = value; OnPropertyChanged(); } }
        public string? PrintConvertToken { get => _printConvertToken; set { _printConvertToken = value; OnPropertyChanged(); } }
        public SeriesCollection SeriesCollection { get => _seriesCollection; set { _seriesCollection = value; OnPropertyChanged(); } }
        public Func<double, string> YFormatter { get; set; }

        ConvertModel convertModel = new();

        #endregion

        public ICommand ConvertToken
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    if (string.IsNullOrWhiteSpace(FirstToken))
                    {
                        PrintConvertToken = "First token can not to be null.";
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(SecondToken))
                    {
                        PrintConvertToken = "Second token can not to be null.";
                        return;
                    }
                    
                    var text = await convertModel.TokenConvert(FirstToken, FirstValueToken, SecondToken);

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        PrintConvertToken = convertModel.ErrorMessage;
                        return;
                    }

                    PrintConvertToken = text;

                    await convertModel.PrintCandlesGraph(FirstToken, SecondToken);
                    
                    if(convertModel.SeriesCollection.Count != 0)
                    {
                        SeriesCollection = convertModel.SeriesCollection;
                        YFormatter = value => value.ToString("C");
                    }
                    else if(convertModel.ErrorMessage.Contains("Candle not found"))
                    {
                        await SlowPrint(convertModel.ErrorMessage);
                        convertModel.ErrorMessage = string.Empty;
                    }
                });
            }

        }    
    }
}