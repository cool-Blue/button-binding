using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using ObservableControls;

namespace button_binding
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ViewModel VM { get; set; }
		public View V { get; set; }
		public MainWindow ()
		{
			InitializeComponent();
			V = new View();
			VM = new ViewModel(button2);
			DataContext = this;
		}

		/// <summary>
		/// Contains pure view state and intra-view actions
		/// has no references to the view, it's bound to by the XAML View
		/// </summary>
		public class View
		{
			public ObservableButton Button0 { get; set; }
			public ObservableButton Button1 { get; set; }
			public ObservableToggleButton Button2 { get; set; }

			public View ()
			{
				Button0 = new ObservableButton(new List<string> { "Paused", "Logging" }, false);
				Button1 = new ObservableButton(new List<string> { "Paused", "Logging" }, false);
				Button2 = new ObservableToggleButton(new List<string> { "Log All", "Log VBA" }, false);
			}
		}

		/// <summary>
		/// mock model
		/// This is a remote control interface bound to a button on the view
		/// </summary>
		public class ViewModel
		{
			public ToggleAutomation Button2 { get; set; }

			public ViewModel (ToggleButton tb)
			{
				Button2 = new ToggleAutomation(tb);
			}
		}
	}
}
