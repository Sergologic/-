using System;
using System.Windows.Input;

// Проект разработан автором Сергей Лысков специально для ООО «ПРОГРАММНЫЙ ЦЕНТР».

namespace SergeiTest.Helpers
{
    // Команда, которая принимает действие для выполнения и условие, можно ли выполнять
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // Что делать при выполнении команды
        private readonly Predicate<object> _canExecute; // Проверка, можно ли выполнить команду

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Проверка, можно ли выполнить команду
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);


        // Событие, чтобы система знала, когда нужно проверить CanExecute заново
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
