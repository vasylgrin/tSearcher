using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using tSearcher.Controller;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    class SearchVM : BaseVM
    {
        #region Property

        private string? _TokenToSearch;
        private Token? printSearchToken;
        private ObservableCollection<Market> printMarkets = new();

        public string? TokenToSearch { get => _TokenToSearch; set { _TokenToSearch = value; OnPropertyChanged("TokenToSearch"); } }
        public Token? PrintSearchToken { get => printSearchToken; set { printSearchToken = value; OnPropertyChanged(); } }
        public ObservableCollection<Market> PrintMarkets { get => printMarkets; set { printMarkets = value; OnPropertyChanged(); } }

        Token currentToken = new();

        #endregion

        JsonController jsonController = new();

        public ICommand SearchButton
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    PrintSearchToken = jsonController.GetTokenForSearch(TokenToSearch);
                    PrintMarkets.Clear();
                    PrintMarkets = jsonController.GetMarkets(PrintSearchToken.FullName.ToLower());
                });
            }
        }
    }
}
