using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    internal class MainWindowVM : BaseVM
    {
        public MainWindowVM()
        {
            SlowOpacity(new Views.HomeUC());
        }
       
        private object currentPage;
        private double _BorderOpacity;

        public object CurrentPage { get => currentPage; set { currentPage = value; OnPropertyChanged(); } } 
        public double BorderOpacity { get => _BorderOpacity; set { _BorderOpacity = value; OnPropertyChanged(); } }


        public ICommand HomeButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Views.HomeUC()));
            }
        }

        public ICommand SearchButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Views.SearchUC()));
            }
        }

        public ICommand ConvertButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Views.ConvertUC()));
            }
        }

        public ICommand OptionsButton
        {
            get
            {
                return new DelegateCommand((obj) => SlowOpacity(new Views.OptionsUC()));
            }
        }

        private async void SlowOpacity(object obj)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    BorderOpacity = i;
                    Task.Delay(1).Wait();
                }

                CurrentPage = obj;

                for (double i = 0.0; i < 1.0; i += 0.1)
                {
                    BorderOpacity = i;
                    Task.Delay(1).Wait();
                }
            });
        }
    }
}
