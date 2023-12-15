using Domain.Models;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class CategoryItemViewModel : ViewModelBase
    {
		public Category Category { get; }
		private string _name;
		private int _id;

		public string Name
		{
			get { return _name; }
			set { _name = value; OnPropertyChanged(); }
		}

		public int Id
		{
			get { return _id; }
			set { _id = value; OnPropertyChanged(); }
		}

        public CategoryItemViewModel(Category category)
        {
			Category = category;
            Id = category.Id;
			Name = category.Name;
        }
    }
}
