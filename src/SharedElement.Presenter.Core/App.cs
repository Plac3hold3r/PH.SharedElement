using MvvmCross.Core.ViewModels;
using SharedElement.Official.Core.ViewModels;

namespace SharedElement.Official.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterNavigationServiceAppStart<ListViewModel>();
    }
}
