using MvvmCross.ViewModels;
using SharedElement.Official.Core.ViewModels;

namespace SharedElement.Official.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterAppStart<ListViewModel>();
    }
}
