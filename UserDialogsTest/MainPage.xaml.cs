using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UserDialogsTest
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			BindingContext = new MainViewModel ();
			InitializeComponent ();
			BarcodeEntry.Completed += HandleCompleted;
		}

		public void HandleCompleted(object sender, EventArgs e)
		{
			(BindingContext as MainViewModel).Barcode = (sender as Entry).Text.Trim ();
		}
	}
}

