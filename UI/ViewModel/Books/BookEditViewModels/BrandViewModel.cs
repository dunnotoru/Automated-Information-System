using Domain.Models;
using Domain.RepositoryInterfaces;
using System;

namespace UI.ViewModel.Books.BookEditViewModels
{
    internal class BrandViewModel : ViewModelBase
    {
		private readonly IBrandRepository _repository;
        private int _id;
		private string _name;

        public BrandViewModel(Brand brand, IBrandRepository repository)
        {
			ArgumentNullException.ThrowIfNull(repository);
            _repository = repository;
			_id = brand.Id;
			_name = brand.Name;
        }

		public Brand GetBrand()
		{
			return _repository.GetById(Id);
		}

        public int Id
		{
			get { return _id; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}

	}
}
