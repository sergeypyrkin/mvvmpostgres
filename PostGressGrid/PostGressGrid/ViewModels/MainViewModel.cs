using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PostGressGrid.Annotations;

namespace PostGressGrid.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string input;

        public string Input
        {
            get { return this.input; }
            set
            {
                this.input = value;
                RaisePropertyChanged();
            }
        }

        private string output;

        public string Output
        {
            get { return this.output; }
            set
            {
                this.output = value;
                RaisePropertyChanged();
            }
        }


        private string number;

        public string Number
        {
            get { return this.number; }
            set
            {
                this.number = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChangeText { get; set; }
        public ICommand OpenWin1 { get; set; }


        public MainViewModel()
        {
            Number = "5";
            ChangeText = new RelayCommand(e =>
            { this.Output = this.Input; },
                o =>
                {
                    return true;
                });

            OpenWin1 = new RelayCommand(e =>
            {
                //var gv = new GridView(Number);
                //gv.ShowDialog();
            },
                o =>
                {
                    return true;
                });
        }






        class RelayCommand : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return this.canExecute == null || this.canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                this.execute(parameter);
            }
        }
    }
}
