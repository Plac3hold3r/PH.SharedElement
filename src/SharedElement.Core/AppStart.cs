using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using SharedElement.Core.ViewModels;

namespace SharedElement.Core
{
    public class AppStart : IMvxAppStart
    {
        private readonly IMvxNavigationService _navigationService;

        public AppStart(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void Start(object hint = null)
        {
            _navigationService.Navigate<ListViewModel>().GetAwaiter().GetResult();
        }
    }
}
