using System.Windows.Controls.Primitives;
using ObservableControls;

namespace ButtonBindingViewModel
{
	/// <summary>
	/// mock model
	/// This is a remote control interface bound to a button on the view
	/// </summary>
	public class ViewModel
	{
		public ToggleAutomation TargetButton { get; set; }

		public ViewModel (ToggleButton tb)
		{
			TargetButton = new ToggleAutomation(tb);
		}
	}
}
