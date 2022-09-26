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

        public string? FirstToken { get { return _firstToken; } set { _firstToken = value; OnPropertyChanged(); } }
        public double? FirstValueToken { get { return _firstValueToken; } set { _firstValueToken = value; OnPropertyChanged(); } }
        public string? SecondToken { get { return _secondToken; } set { _secondToken = value; OnPropertyChanged(); } }
        public string? PrintConvertToken { get => _printConvertToken; set { _printConvertToken = value; OnPropertyChanged(); } }

        ConvertModel convertModel = new();

        #endregion

        public ICommand ConvertToken
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    PrintConvertToken = convertModel.TokenConvert(FirstToken, FirstValueToken, SecondToken);
                });
            }

        }
    }
}