using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FatSubs.Data
{
	public class BusinessViewModel : BindableObject
	{
		public BusinessViewModel()
		{
			Staff = new List<StaffViewModel>();
			Menu = new List<MenuItemViewModel>();
		}
			
		public string Name {get;set;}
		public HoursViewModel Hours { get; set; }
		public LocationViewModel Location { get; set; }
		public ICollection<StaffViewModel> Staff { get; set; }
		public ICollection<MenuItemViewModel> Menu { get; set; }
	}
}

