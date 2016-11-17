using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using button_binding.ViewModels;
using ObservableControls;

namespace button_binding.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableButton Button0 { get; set; }
		public ObservableButton Button1 { get; set; }
		public ObservableToggleButton Button2 { get; set; }

		/// <summary>
		/// Contains pure view state and UI behaviour
		/// has no references to the view, it's bound to by the XAML View
		/// </summary>
		public MainWindow ()
		{
			// build the view
			Button0 = new ObservableButton(new List<string> { "Paused", "Logging" }, false);
			Button1 = new ObservableButton(new List<string> { "Paused", "Logging" }, false);
			Button2 = new ObservableToggleButton(new List<string> { "Log All", "Log VBA" }, false);

			InitializeComponent();

			// build the view model and connect it to the view
			DataContext = new MainViewModel(button2);
		}
	}

}