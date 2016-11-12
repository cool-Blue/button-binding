using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableControls
{
	public abstract class ObservableObjectBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual Boolean SetProperty<T>(ref T backingMember, T value, 
			[CallerMemberName] string propName = "")
		{
			if (Object.Equals(backingMember, value)) return false;

			backingMember = value;
			this.OnPropertyChanged(propName);
			return true;

		}

		protected virtual void OnPropertyChanged (
			[CallerMemberName] string propName = "")
		{
			var pc = PropertyChanged;
			if (pc != null)
				pc(this, new PropertyChangedEventArgs(propName));
		}
	}
}
