using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tSearcher.ViewModels
{
    internal class BaseVM : INotifyPropertyChanged
    {
        private double _borderOpacity;
        private object _printText;

        public double BorderOpacity
        {
            get { return _borderOpacity; }
            set { _borderOpacity = value; OnPropertyChanged(); }
        }
        public object PrintObject
        {
            get { return _printText; }
            set { _printText = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected async Task SlowPrint(object print)
        {
            await Task.Factory.StartNew(() =>
            {
                PrintObject = "";

                for (double i = 0.0; i < 1; i += 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(50);
                }

                PrintObject = print;
                Thread.Sleep(5000);

                for (double i = 1; i > 0.0; i -= 0.1)
                {
                    BorderOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
