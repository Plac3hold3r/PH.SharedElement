using MvvmCross.Core.ViewModels;
using SharedElement.Core.ViewModels;

namespace SharedElement.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterAppStart<ListViewModel>();
    }
}
