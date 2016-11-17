using System;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ControlAutomation
{
	/// <summary>
	/// Wraps an Action delegate in an ICommand 
	/// </summary>
	public class DelegateCommand : ICommand
	{
		public bool CanExecute (object parameter)
		{
			return true;
		}

		private readonly Action<object> _execute;
		public void Execute (object parameter)
		{
			_execute(parameter);
		}

		public event EventHandler CanExecuteChanged;

		public DelegateCommand (Action<object> execute)
		{
			_execute = execute;
		}
	}

	/// <summary>
	/// provides an automation object bound to a ToggleButton
	/// </summary>
	public class ToggleAutomation
	{
		private void _push (object o)
		{
			var peer = new ToggleButtonAutomationPeer(_tb);
			var toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
			if (toggleProvider != null) toggleProvider.Toggle();
		}

		public DelegateCommand Push { get; private set; }

		private readonly ToggleButton _tb;
		public ToggleAutomation (ToggleButton tb)
		{
			_tb = tb;
			Push = new DelegateCommand(_push);
		}
	}
}
