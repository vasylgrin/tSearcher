using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    class SearchVM : BaseVM
    {
        #region Property

        private string? _TokenToSearch;
        private string? printSearchToken;
        private ObservableCollection<Market> printMarkets = new();

        public string? TokenToSearch { get => _TokenToSearch; set { _TokenToSearch = value; OnPropertyChanged("TokenToSearch"); } }
        public string? PrintSearchToken { get => printSearchToken; set { printSearchToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Market> PrintMarkets { get => printMarkets; set { printMarkets = value; OnPropertyChanged(); } }

        Token currentToken = new();

        #endregion

        JsonModel jsonController = new();

        public ICommand SearchButton
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var token = jsonController.GetTokenForSearch(TokenToSearch);
                    if (!string.IsNullOrWhiteSpace(token.FullName))
                    {
                        SlowPrint(token.ToString());
                        PrintMarkets.Clear();
                        PrintMarkets = jsonController.GetMarkets(token.FullName.ToLower());
                    }
                    else
                    {
                        SlowPrint($"{TokenToSearch} is not found");
                        printMarkets.Clear();
                    }
                });
            }
        }

        
    }
}
