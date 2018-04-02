using MvvmCross.Core.ViewModels;
using SharedElement.Presenter.Core.ViewModels;

namespace SharedElement.Presenter.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterNavigationServiceAppStart<ListViewModel>();
    }
}
