using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using SharedElement.Official.Core;

#if DEBUG
[assembly: Application(Debuggable = true, Label = "@string/app_name", Description = "@string/app_description")]
#else
[assembly: Application(Debuggable = false, Label = "@string/app_name", Description = "@string/app_description")]
#endif

namespace SharedElement.Official.Droid
{
    public class Setup : MvxAppCompatSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
            => new App();

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
        };
    }
}
