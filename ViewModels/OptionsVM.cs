using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using tSearcher.Models;

namespace tSearcher.ViewModels
{
    internal class OptionsVM : BaseVM
    {
        private ResourceDictionary resourceDictionary = new();

        public ResourceDictionary ResourceDictionary
        {
            get { return resourceDictionary; }
            set { resourceDictionary = value; OnPropertyChanged(); }
        }




        public ICommand LightTheme
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var uri = new Uri("styles/Light.xaml", UriKind.Relative);
                    ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;

                    ResourceDictionary = resourceDictionary;
                });
            }
        }

        public ICommand DarkTheme
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    var uri = new Uri("styles/Dark.xaml", UriKind.Relative);
                    ResourceDictionary resourceDictionary = Application.LoadComponent(uri) as ResourceDictionary;

                    ResourceDictionary = resourceDictionary;

                });
            }
        }
    }
}
