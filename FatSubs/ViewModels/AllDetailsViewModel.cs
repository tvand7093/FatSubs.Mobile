using System;
using FatSubs.Data;
using Xamarin.Forms;
using System.Threading.Tasks;
using FatSubs.Services;
using System.Linq;

namespace FatSubs.ViewModels
{
	public class AllDetailsViewModel : BindableObject
	{
		public const string FetchDataMessage = "FetchData";

		public static readonly BindableProperty ModelProperty =
			BindableProperty.Create <AllDetailsViewModel, BusinessViewModel>(m => m.Model, null);

		public BusinessViewModel Model {
			get { return GetValue (ModelProperty) as BusinessViewModel; }
			set { SetValue (ModelProperty, value); }
		}

		private async Task FetchData() {
			Application.Current.MainPage.IsBusy = true;

			var apiService = new FatSubsService ();
			var apiResult = await apiService.GetDetailsAsync ();
			if (apiResult != null) {
				Model = apiResult;

				Model.Menu.ElementAt (0).Image = "https://scontent.fsnc1-1.fna.fbcdn.net/hphotos-xaf1/v/t1.0-9/12321167_1706479812930659_6002975706401124688_n.jpg?oh=c07c10bb72646346e199219d9aef28b0&oe=57031592";
				Model.Menu.ElementAt (1).Image = "https://scontent.fsnc1-1.fna.fbcdn.net/hphotos-xap1/v/t1.0-9/12342738_1704858753092765_4251027066866599800_n.jpg?oh=73eb5f97a585576e22a4997ceb9d5b57&oe=573E8018";
				Model.Menu.ElementAt(2).Image = @"https://scontent.fsnc1-1.fna.fbcdn.net/hphotos-xlp1/v/t1.0-9/12342563_1708706116041362_3877232908512759184_n.jpg?oh=a401b87183dbd975413ba18b6e168205&oe=56FB7656";
			}

			Application.Current.MainPage.IsBusy = false;
		}

		public AllDetailsViewModel ()
		{
			MessagingCenter.Subscribe<AllDetailsViewModel> (this, FetchDataMessage,
				async (thisViewModel) => {
					await thisViewModel.FetchData();
				});
		}
	}
}

