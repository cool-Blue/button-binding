using System.Windows.Controls.Primitives;
using ObservableControls;

namespace ViewModel
{
	/// <summary>
	/// mock model
	/// This is a remote control interface bound to a button on the view
	/// </summary>
	public class VM
	{
		public ToggleAutomation TargetButton { get; set; }

		public VM (ToggleButton tb)
		{
			TargetButton = new ToggleAutomation(tb);
		}
	}
}
