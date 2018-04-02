using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace SharedElement.Official.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void ShowListView()
        {
            _navigationService.Navigate<ListViewModel>();
        }
    }
}
