using MvvmCross.Core.ViewModels;
using SharedElement.Custom.Core.ViewModels;

namespace SharedElement.Custom.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterNavigationServiceAppStart<ListViewModel>();
    }
}
