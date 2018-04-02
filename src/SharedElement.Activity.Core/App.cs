using MvvmCross.Core.ViewModels;
using SharedElement.Activity.Core.ViewModels;

namespace SharedElement.Activity.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterNavigationServiceAppStart<ListViewModel>();
    }
}
