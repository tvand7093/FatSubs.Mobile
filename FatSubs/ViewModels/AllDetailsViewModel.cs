using System;
using FatSubs.Data;
using Xamarin.Forms;
using System.Threading.Tasks;
using FatSubs.Services;
using System.Linq;
using Xamarin.Forms.Maps;
using System.Windows.Input;

namespace FatSubs.ViewModels
{
	public class AllDetailsViewModel : BindableObject
	{
		public const string FetchDataMessage = "FetchData";

		public ICommand UpdateMapCommand { get; set; }

		public static readonly BindableProperty ModelProperty =
			BindableProperty.Create <AllDetailsViewModel, BusinessViewModel>(m => m.Model, null);

		public BusinessViewModel Model {
			get { return GetValue (ModelProperty) as BusinessViewModel; }
			set { SetValue (ModelProperty, value); }
		}

		public Position Portland { get; private set; }

		private async Task FetchData() {
			Application.Current.MainPage.IsBusy = true;

			var apiService = new FatSubsService ();
			var apiResult = await apiService.GetDetailsAsync ();
			if (apiResult != null) {
				Model = apiResult;
				var coder = new Geocoder ();
				var portlandPositions = await coder.GetPositionsForAddressAsync ("Portland, Oregon");
				if (portlandPositions.Count () > 0) {
					Portland = portlandPositions.ElementAt (0);
				}

				var positions = await coder.GetPositionsForAddressAsync (Model.Location.Address);
				if (positions.Count () > 0) {
					foreach (var pos in positions) {
						var location = new Pin () {
							Position = pos,
							Address = Model.Location.Address,
							Type = PinType.Place,
							Label = Model.Location.Name
						};

						if (UpdateMapCommand != null) {
							UpdateMapCommand.Execute (location);
						}
					}
				}

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

