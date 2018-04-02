using System;
using System.Collections.Generic;
using System.Reflection;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace SharedElement.Official.Droid
{
    public class SharedElementCompatViewPresenter : MvxAppCompatViewPresenter
    {
        public SharedElementCompatViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
        }
        protected override void ShowFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            base.ShowFragment(view, attribute, request);
        }
    }
}
