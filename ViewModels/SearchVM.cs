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
                return new DelegateCommand(async obj =>
                {
                    if (string.IsNullOrWhiteSpace(TokenToSearch))
                    {
                        await SlowPrint($"Enter token for search");
                        return;
                    }
                    
                    jsonController.Token = null;
                    await jsonController.GetTokenForSearch(TokenToSearch);
                    
                    if (jsonController.Token != null)
                    {
                        SlowPrint(jsonController.Token.ToString());
                        PrintMarkets.Clear();
                        PrintMarkets = await jsonController.GetMarkets(jsonController.Token.FullName.ToLower());
                    }
                    else
                    {
                        await SlowPrint($"{TokenToSearch} is not found");
                        printMarkets.Clear();
                    }
                });
            }
        }

        
    }
}
