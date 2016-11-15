using System;
using System.Collections.Generic;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace ObservableControls
{
	
	/// <summary>
	/// adds an ICommand delegate binding for a button
	/// </summary>
	public class ObservableButton : ObservableButtonBase
	{
		public new Boolean On { set; private get; }

		public ObservableButton (List<string> options, Boolean on = true)
			: base(options, on)
		{
			DelegateClick = new DelegateCommand(_click);
		}

		void _click (object sender)
		{
			On = !On;
			Content = On ? Options[0] : Options[1];
		}

		public DelegateCommand DelegateClick { get; private set; }

	}

	/// <summary>
	/// Adds On binding for a target togglebutton
	/// </summary>
	public class ObservableToggleButton : ObservableButtonBase
	{
		private Boolean _on;

		public new Boolean On
		{
			private get { return _on; }
			set
			{
				if (_on == value) return;
				_on = value;
				Content = value ? Options[0] : Options[1];
			}
		}

		public ObservableToggleButton(List<string> options, Boolean on = true)
			: base(options, on) { }

	}

	/// <summary>
	/// Toggles Content between two values in options
	/// Exposes On and Content for binding to the view
	/// need to add a command or a binding to drive On
	/// On drives Content
	/// </summary>
	public abstract class ObservableButtonBase : ObservableObjectBase
	{
		public List<string> Options { get; set; }

		private string _content;

		public string Content
		{
			get { return _content; }
			set
			{
				SetProperty(ref _content, value);
			}
		}

		public Boolean On { set; private get; }

		protected ObservableButtonBase (List<string> options, Boolean on = true)
		{
			Options = options;
			On = on;
			Content = On ? Options[0] : Options[1];
		}
	}

	/// <summary>
	/// Wraps an Action delegate in an ICommand 
	/// </summary>
	public class DelegateCommand : ICommand
	{
		public bool CanExecute(Object parameter)
		{
			return true;
		}

		private readonly Action<Object> _execute;
		public void Execute(Object parameter)
		{
			_execute((Object) parameter);
		}

		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<Object> execute)
		{
			_execute = execute;
		}
	}

	/// <summary>
	/// provides an automation object bound to a ToggleButton
	/// </summary>
	public class ToggleAutomation
	{
		private void _push (Object o)
		{
			var peer = new ToggleButtonAutomationPeer(_tb);
			var toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
			if (toggleProvider != null) toggleProvider.Toggle();
		}

		public DelegateCommand Push { get; private set; }

		private readonly ToggleButton _tb;
		public ToggleAutomation(ToggleButton tb)
		{
			_tb = tb;
			Push = new DelegateCommand(_push);
		}
	}

	public enum State
	{
		Pressed,
		Released
	}
}
