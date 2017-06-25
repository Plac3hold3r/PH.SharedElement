using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace SharedElement.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
            => RegisterAppStart(Mvx.IocConstruct<AppStart>());
    }
}
