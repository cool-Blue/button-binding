using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableControls
{
	public abstract class ObservableObjectBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual bool SetProperty<T>(ref T backingMember, T value, 
			[CallerMemberName] string propName = "")
		{
			if (Equals(backingMember, value)) return false;

			backingMember = value;
			OnPropertyChanged(propName);
			return true;

		}

		protected virtual void OnPropertyChanged (string propName)
		{
			var pc = PropertyChanged;
			if (pc != null)
				pc(this, new PropertyChangedEventArgs(propName));
		}
	}
}
