using System;

namespace Dexuse.ICommand
{
    public abstract class ObservableCommand : System.Windows.Input.ICommand
    {
        ///<summary>
        /// The event fired when a command is executed;
        ///</summary>
        public event EventHandler CommandExecuted = delegate { };
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null. </param>
        public void Execute(object parameter)
        {
            ExecuteCommand(parameter);
            CommandExecuted(this, EventArgs.Empty);
        }
        protected abstract void ExecuteCommand(object parameter);
        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state. 
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.
        ///                 </param>
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };
    }
}
