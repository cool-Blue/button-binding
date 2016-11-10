using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace button_binding
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static MainWindow Instance;

		public MainWindow ()
		{
			InitializeComponent();
			DataContext = new ViewModel
			{
				Button1 = new ViewModel.ObservableButton(button1, new List<string> { "Paused", "Logging" }, false),
				Button2 = new ViewModel.ObservableToggleButton(button2, new List<string> { "Log All", "Log VBA" }, false),
			};
		}

		public class ViewModel : INotifyPropertyChanged
		{
			private static ViewModel _instance;

			public event PropertyChangedEventHandler PropertyChanged;

			protected virtual void OnPropertyChanged<T> (T control, [CallerMemberName] string propName = "")
			{
				var pc = PropertyChanged;
				if (pc != null)
					pc(control, new PropertyChangedEventArgs(propName));
			}

			public class ObservableButton
			{
				private readonly Button _b;
				private readonly List<string> _options;

				private string _content;
				public string Content
				{
					get { return _content; }
					set
					{
						if (_content == value) return;
						_content = value;
						_instance.OnPropertyChanged(this);
					}
				}

				public Boolean On { set; private get; }

				public ObservableButton (Button b, List<string> options, Boolean on = true)
				{
					_b = b;
					_options = options;
					_b.Click += Click;
					On = on;
					Content = On ? _options[0] : _options[1];
				}
				public void Click (object sender, RoutedEventArgs e)
				{
					On = !On;
					Content = On ? _options[0] : _options[1];
				}

			}

			public class ObservableToggleButton
			{
				private readonly ToggleButton _b;
				private readonly List<string> _options;

				private string _content;
				public string Content
				{
					get { return _content; }
					private set
					{
						if (_content == value) return;
						_content = value;
						_instance.OnPropertyChanged(this);
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

			}

			public ObservableButton Button1 { get; set; }

			public ObservableToggleButton Button2 { get; set; }

			public ViewModel ()
			{
				_instance = this;
			}
		}
	}
}
