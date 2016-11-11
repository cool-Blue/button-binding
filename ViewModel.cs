using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

public class Vm
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propName = "")
		{
			var pc = PropertyChanged;
			if (pc != null)
				pc(this, new PropertyChangedEventArgs(propName));
		}
	}

	public class ObservableButton : ObservableObject
	{
		private readonly List<string> _options;

		private string _content;
		public string Content
		{
			get { return _content; }
			set
			{
				if (_content == value) return;
				_content = value;
				OnPropertyChanged();
			}
		}

		public Boolean On { set; private get; }

		public ObservableButton (List<string> options, Boolean on = true)
		{
			_options = options;
			On = on;
			Content = On ? _options[0] : _options[1];
		}
		public void Click (object sender, RoutedEventArgs e)
		{
			On = !On;
			Content = On ? _options[0] : _options[1];
		}

	}

	public class ObservableToggleButton : ObservableObject
	{
		private readonly List<string> _options;

		private string _content;
		public string Content
		{
			get { return _content; }
			private set
			{
				if (_content == value) return;
				_content = value;
				OnPropertyChanged();
			}
		}

		private Boolean _on;
		public Boolean On
		{
			private get { return _on; }
			set
			{
				if (_on == value) return;
				_on = value;
				Content = value ? _options[0] : _options[1];
			}
		}

		public ObservableToggleButton (ToggleButton b, List<string> options, Boolean on = true)
		{
			_b = b;
			_options = options;
			On = on;
			Content = _b.IsChecked ?? false ? _options[0] : _options[1];
		}

		public void Push ()
		{
			var peer = new ToggleButtonAutomationPeer(_b);
			var toggleProvider = peer.GetPattern(PatternInterface.Toggle) as IToggleProvider;
			if (toggleProvider != null) toggleProvider.Toggle();
		}
	}

	public ObservableButton Button1 { get; set; }

	public ObservableToggleButton Button2 { get; set; }

	public Vm (Button button, ToggleButton togglebutton)
	{
	}
}
