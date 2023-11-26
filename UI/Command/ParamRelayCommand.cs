using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.Command
{
    public class ParamRelayCommand<T> : CommandBase
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private Action<T> _methodToExecute;
        private Func<bool> _canExecuteEvaluator;
        public ParamRelayCommand(Action<T> methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }
        public ParamRelayCommand(Action<T> methodToExecute)
            : this(methodToExecute, null)
        {
        }
        public override bool CanExecute(object parameter)
        {
            if (_canExecuteEvaluator == null)
            {
                return true;
            }
            else
            {
                bool result = _canExecuteEvaluator.Invoke();
                return result;
            }
        }
        public override void Execute(object parameter)
        {
            _methodToExecute.Invoke((T)parameter);
        }
    }
}
