using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableControls
{
	public abstract class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propName = "")
		{
			var pc = PropertyChanged;
			if (pc != null)
				pc(this, new PropertyChangedEventArgs(propName));
		}
	}
}
