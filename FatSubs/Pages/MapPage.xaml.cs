using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using FatSubs.ViewModels;

namespace FatSubs.Pages
{
	public partial class MapPage : ContentPage
	{
		private Map map;

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			if (BindingContext is AllDetailsViewModel) {
				var vm = BindingContext as AllDetailsViewModel;
				vm.UpdateMapCommand = new Command <Pin>((toAdd) => {
					//get the map view
					if(map.VisibleRegion == null || map.VisibleRegion.Center != vm.Portland){
						map.MoveToRegion(MapSpan.FromCenterAndRadius(vm.Portland, Distance.FromMiles(5)));
					}

					map.Pins.Add(toAdd);
				});
			}
		}

		public MapPage ()
		{
			InitializeComponent ();
			map = Content as Map;
		}
	}
}

