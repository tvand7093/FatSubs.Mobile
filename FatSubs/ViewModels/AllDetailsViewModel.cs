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
			BindableProperty.Create ("Model", 
	            typeof(BusinessViewModel),
	            typeof(AllDetailsViewModel),
	            null);

		public BusinessViewModel Model {
			get { return GetValue (ModelProperty) as BusinessViewModel; }
			set { SetValue (ModelProperty, value); }
		}

		public static readonly BindableProperty RefreshingProperty =
			BindableProperty.Create ("Refreshing", 
				typeof(bool),
				typeof(AllDetailsViewModel),
				false);

		public bool Refreshing {
			get { return (bool)GetValue (RefreshingProperty); }
			set { SetValue (RefreshingProperty, value); }
		}

		public ICommand RefreshCommand { get; private set; }

		public Position Portland { get; private set; }

		private async Task FetchData() {
			var apiService = new FatSubsService ();
			var apiResult = await apiService.GetDetailsAsync ();
			if (apiResult != null) {
				Model = apiResult;
				var coder = new Geocoder ();
				var portlandPositions = await coder.GetPositionsForAddressAsync ("Portland, Oregon");
				if (portlandPositions.Any()) {
					Portland = portlandPositions.ElementAt (0);
				}

				var positions = await coder.GetPositionsForAddressAsync (Model.Location.Address);
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

		public AllDetailsViewModel ()
		{
			RefreshCommand = new Command (async () => {
				Application.Current.MainPage.IsBusy = Refreshing = true;

				await FetchData();

				Application.Current.MainPage.IsBusy = Refreshing = false;
			});
		}
	}
}

