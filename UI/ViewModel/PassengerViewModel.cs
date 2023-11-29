namespace UI.ViewModel
{
    public class PassengerViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => "amongus";
            set
            {
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }
}
