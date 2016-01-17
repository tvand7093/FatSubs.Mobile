using System;

using Xamarin.Forms;
using FatSubs.Pages;
using Xamarin.Forms.Xaml;
using FatSubs.ViewModels;


[assembly: XamlCompilation (XamlCompilationOptions.Compile)]

namespace FatSubs
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new CustomNavigationPage (new CustomTabbedPage());
		}


		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

