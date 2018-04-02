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

        public void Test()
        {
            _navigationService.Navigate<ListViewModel>();
        }
    }
}
