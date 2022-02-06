using Module1_FinalTask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Module1_FinalTask.Models;

namespace Module1_FinalTask.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Calculator calc = new Calculator();
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        //Инициализация первого операнда
        private double numOp1;
        public double NumOp1
        {
            get => numOp1;
            set
            {
                numOp1 = value;
                OnPropertyChanged();
            }
        }

        //Инициализация второго операнда
        private double numOp2;
        public double NumOp2
        {
            get => numOp2;
            set
            {
                numOp2 = value;
                OnPropertyChanged();
            }
        }

        //Инициализация результата вычислений
        private string result = "0";
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        //Инициализация вывода текущих вычислений
        private string lastCalc;
        public string LastCalc
        {
            get => lastCalc;
            set
            {
                lastCalc = value;
                OnPropertyChanged();
            }
        }
        
       //Конструкторы команд
        public ICommand NumberCommand { get; }
        private void OnNumberCommandExecute(object p)
        {
            Result = calc.Number(p);
        }
        private bool CanNumberCommandExecuted(object p)
        {
            return p!=null;
        }

        public ICommand AriphCommand { get; set; }
        private void OnAriphCommandExecute(object p)
        {
            LastCalc = calc.Ariph((string)p);
            Result = calc.Result();
        }
        private bool CanAriphCommandExecuted(object p)
        {
            return !calc.Error();
        }

        public ICommand CalcCommand { get; set; }
        private void OnCalcCommandExecute(object p)
        {
            LastCalc = calc.Calc((string)p);
            Result = calc.Result();
        }
        private bool CanCalcCommandExecuted(object p)
        {
            return !calc.Error();
        }


        public ICommand ClearCommand { get; }
        private void OnClearCommandExecute(object p)
        {
            calc.CalcClear();
            LastCalc = calc.LastCalc();
            Result = calc.Result();

        }
        private bool CanClearCommandExecuted(object p)
        {
            return true;
        }

        //Инициализация основного окна
        public MainWindowViewModel()
        {
            //Инициализация команд
            NumberCommand = new RelayCommand(OnNumberCommandExecute, CanNumberCommandExecuted);
            AriphCommand = new RelayCommand(OnAriphCommandExecute, CanAriphCommandExecuted);
            ClearCommand = new RelayCommand(OnClearCommandExecute, CanClearCommandExecuted);
            CalcCommand = new RelayCommand(OnCalcCommandExecute, CanCalcCommandExecuted);
        }
    }
}
