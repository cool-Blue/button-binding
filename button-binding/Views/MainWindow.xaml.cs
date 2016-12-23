using System.Collections.Generic;
using button_binding.ViewModels;
using ContentToggleButton;

namespace button_binding.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public StaticButton Button0 { get; set; }
		public StaticButton Button1 { get; set; }
		public StaticButton Button2 { get; set; }
		public StaticButton ModelButton { get; set; }

		/// <summary>
		/// Contains pure view state and UI behaviour
		/// has no references to the view, it's bound to by the XAML View
		/// </summary>
		public MainWindow ()
		{
			// build the view
			Button0 = new StaticButton(new List<string> { null, "Logging" }, false);
			Button1 = new StaticButton(new List<string> { "Paused", "Logging" }, false);
			Button2 = new StaticButton(new List<string> { "Log All", "Log VBA" }, false);
			ModelButton = new StaticButton(new List<string> { "MODEL", "MODEL" }, false);

			InitializeComponent();

			// build the view model and connect it to the view
			DataContext = new ViewModel(button2);
		}
	}
}

