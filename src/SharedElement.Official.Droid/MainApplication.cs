using System;
using Android.App;
using Android.Runtime;
using MvvmCross.Droid.Support.V7.AppCompat;
using SharedElement.Official.Core;

namespace SharedElement.Official.Droid
{
#if DEBUG
    [Application(Debuggable = true, Label = "@string/app_name", Description = "@string/app_description")]
#else
    [Application(Debuggable = false, Label = "@string/app_name", Description = "@string/app_description")]
#endif
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
    }
}
