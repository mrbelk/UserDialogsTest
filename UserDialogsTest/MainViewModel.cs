using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace UserDialogsTest
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel ()
		{
			IsBusy = false;

			ConnectCommand = new Command (
				async(nothing) => {
					Status = "Connecting...";
					IsBusy = true;
					await Task.Delay(3000).ContinueWith(async(t) => {
						Device.BeginInvokeOnMainThread(() => {
							IsBusy = false;
							Status = "Connected";});
					});
				},
				(nothing) => {
					return !IsBusy;
				});
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		private void OnNotifyPropertyChanged([CallerMemberName] string property="") {
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (property));
		}

		public ICommand ConnectCommand {
			get;
			protected set;
		}

		private bool _isBusy;
		public bool IsBusy {
			get { return _isBusy; }
			set{
				if (_isBusy == value)
					return;

				_isBusy = value;

				OnNotifyPropertyChanged ();

				if (_isBusy)
					UserDialogs.Instance.ShowLoading ("Working...");
				else
					UserDialogs.Instance.HideLoading ();

				((Command)ConnectCommand).ChangeCanExecute ();
			}
		}

		private string _barcode;
		public string Barcode {
			get { return _barcode; }
			set {
				if ((_barcode ?? "") == (value ?? ""))
					return;

				_barcode = value;
				OnNotifyPropertyChanged ();
			}
		}

		private string _status;
		public string Status {
			get { return _status; }
			set {
				if ((_status ?? "") == (value ?? ""))
					return;

				_status = value ?? "";
				OnNotifyPropertyChanged ();
			}
		}
	}
}