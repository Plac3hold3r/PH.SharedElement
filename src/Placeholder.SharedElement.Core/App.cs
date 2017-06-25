using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Placeholder.SharedElement.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterAppStart(Mvx.IocConstruct<AppStart>());
    }
}
