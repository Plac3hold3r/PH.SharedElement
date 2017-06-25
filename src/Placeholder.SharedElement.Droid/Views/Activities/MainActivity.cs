﻿using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using SharedElement.Core.ViewModels;

namespace SharedElement.Droid.Views
{
    [Activity(
        Theme = "@style/AppTheme",
        Name = DroidConstants.SharedElement_Views_Namespace + nameof(MainActivity))]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
        }
    }
}