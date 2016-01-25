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
			(BindingContext as AllDetailsViewModel).RefreshCommand.Execute(null);
		}

		public CustomTabbedPage ()
		{
			BindingContext = new AllDetailsViewModel ();

			Children.Add (new MenuPage ());
			Children.Add (new AllDetailsPage ());
			Children.Add (new MapPage ());
			Children.Add (new StaffPage ());
		}
	}
}

