using Domain.Models;
using System;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class StationItemViewModel : ViewModelBase
    {
		public Station Station { get; }
		private int _id;
		private string _name;
		private string _address;

		public int Id
		{
			get { return _id; }
			set { _id = value; OnPropertyChanged(); }
		}
		
		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}
		
		public string Address
		{
			get { return _address; }
			set { _address = value; OnPropertyChanged(); }
		}

        public StationItemViewModel(Station station)
        {
            ArgumentNullException.ThrowIfNull(station);

			Station = station;
			Id = station.Id;
			Name = station.Name ?? "";
			Address = station.Address ?? "";
        }
    }
}