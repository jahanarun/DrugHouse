using System;
using Dexuse.ICommand;

namespace DrugHouse.ViewModel.Common
{
    internal class ExecutedCommand : ObservableCommand
    {
        private readonly Action Action;
        private readonly Func<bool> Predicate;

        public ExecutedCommand(Action action, Func<bool> canExecute )
        {
            Action = action;
            Predicate = canExecute;

        }
        protected override void ExecuteCommand(object parameter)
        {
            Action.Invoke();
        }

        public override bool CanExecute(object parameter)
        {
            return Predicate.Invoke();
        }
    }
}