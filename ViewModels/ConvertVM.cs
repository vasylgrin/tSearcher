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
                    PrintConvertToken = convertModel.TokenConvert(FirstToken, FirstValueToken, SecondToken, out bool isOk);
                    
                    if (isOk)
                    {
                        SlowPrint(convertModel.PrintCandlesGraph(FirstToken, SecondToken, out SeriesCollection seriesCollection));
                        SeriesCollection = seriesCollection;
                        YFormatter = value => value.ToString("C");
                    }
                });
            }

        }    
    }
}