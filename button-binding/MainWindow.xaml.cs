using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ButtonBindingViewModel;
using ObservableControls;

namespace button_binding
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
			DataContext = new ViewModel(button2);
		}
	}
}

namespace converters
{
	public class ResourceDictionaryMembersToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
	public class NoOpConverter : IValueConverter
	{
		public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}
