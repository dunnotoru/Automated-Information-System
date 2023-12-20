using Domain.Models;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    public class CategoryItemViewModel : ViewModelBase
    {
		private bool _isSelected;
		private CategoryViewModel _category;

        public CategoryItemViewModel(CategoryViewModel category)
        {
            _category = category;
        }

		public bool IsSelected
		{
			get { return _isSelected; }
			set { _isSelected = value; OnPropertyChanged(); }
		}
        
        public CategoryViewModel Category
        {
            get { return _category; }
        }
    }
}
