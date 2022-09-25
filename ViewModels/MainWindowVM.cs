using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using tSearcher.Controller;

namespace tSearcher.ViewModels
{
    internal class MainWindowVM : BaseVM
    {
		private object currentPage;
        private double _BorderOpacity;


        public object CurrentPage
        {
			get { return currentPage; }
			set { currentPage = value; OnPropertyChanged(); }
		}
        public double BorderOpacity { get => _BorderOpacity; set { _BorderOpacity = value; OnPropertyChanged(); } }


        public ICommand HomeButton
		{
			get
			{
				return new DelegateCommand((obj) => CurrentPage = new Views.HomeUC());
			}
		}

        public ICommand SearchButton
        {
            get
            {
                return new DelegateCommand((obj) => CurrentPage = new Views.SearchUC());
            }
        }       
    }
}
