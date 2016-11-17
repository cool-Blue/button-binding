using System.Collections.Generic;
using ControlAutomation;


namespace ObservableControls
{
	
	/// <summary>
	/// adds an ICommand delegate binding for a button
	/// </summary>
	public class ObservableButton : ObservableButtonBase
	{
		public new bool On { set; private get; }

		public ObservableButton (List<string> options, bool on = true)
			: base(options, on)
		{
			DelegateClick = new DelegateCommand(_click);
		}

		private void _click (object sender)
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
		private bool _on;

		public new bool On
		{
			set
			{
				if (_on == value) return;
				_on = value;
				Content = value ? Options[0] : Options[1];
			}
		}

		public ObservableToggleButton(List<string> options, bool on = true)
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

		public bool On { set; private get; }

		protected ObservableButtonBase (List<string> options, bool on = true)
		{
			Options = options;
			On = on;
			Content = On ? Options[0] : Options[1];
		}
	}

}
