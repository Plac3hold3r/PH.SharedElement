using MvvmCross.Core.ViewModels;
using SharedElement.Core.Navigation;

namespace SharedElement.Core.ViewModels
{
    public class DetailsViewModel : MvxViewModel
    {
        public void Init(GoToDetailsParameters parameters)
        {
            Title = parameters.Title;
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
