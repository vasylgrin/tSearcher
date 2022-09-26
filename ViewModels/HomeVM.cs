using MyCryptoApp.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    internal class HomeVM : BaseVM
    {
        private ObservableCollection<Token> tokens = new();
        public ObservableCollection<Token> Tokens { get => tokens; set { tokens = value; OnPropertyChanged(); } }

        public HomeVM()
        {
            GetTokens();
        }

        public ICommand UpdateTopTokensButton
        {
            get
            {
                return new DelegateCommand((obj) => GetTokens());
            }
        }

        private void GetTokens()
        {
            Tokens.Clear();
            Tokens = new JsonModel().GetTopToken(10);
        }
    }
}
