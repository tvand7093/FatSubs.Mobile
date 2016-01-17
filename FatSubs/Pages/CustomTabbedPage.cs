using System;
using Xamarin.Forms;
using FatSubs.ViewModels;

namespace FatSubs.Pages
{
	public class CustomTabbedPage : TabbedPage
	{
		protected override void OnCurrentPageChanged ()
		{
			base.OnCurrentPageChanged ();
			Title = CurrentPage.Title;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			//trigger fetching of data.
			MessagingCenter.Send (BindingContext as AllDetailsViewModel, AllDetailsViewModel.FetchDataMessage);
		}

		public CustomTabbedPage ()
		{
			BindingContext = new AllDetailsViewModel ();

			Children.Add (new AllDetailsPage ());
			Children.Add (new StaffPage ());
			Children.Add (new MenuPage ());
		}
	}
}

