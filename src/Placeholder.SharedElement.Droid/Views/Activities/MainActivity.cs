using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Shared.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Platform.Exceptions;
using Placeholder.SharedElement.Core;
using Placeholder.SharedElement.Core.ViewModels;

namespace Placeholder.SharedElement.Droid.Views
{
    [Android.App.Activity(
        Theme = "@style/AppTheme",
        Name = DroidConstants.SharedElement_Views_Namespace + nameof(MainActivity))]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SetTitle(Resource.String.app_project_name);
        }

        public override bool Show(MvxViewModelRequest request, Bundle bundle, Type fragmentType, MvxFragmentAttribute fragmentAttribute)
        {
            var fragmentTag = GetFragmentTag(request, bundle, fragmentType);
            FragmentCacheConfiguration.RegisterFragmentToCache(fragmentTag, fragmentType, request.ViewModelType, fragmentAttribute.AddToBackStack);

            var controlTags = string.Empty;
            request.PresentationValues?.TryGetValue(SharedConstants.Animate_Tag, out controlTags);

            ShowFragment(fragmentTag, fragmentAttribute.FragmentContentId, bundle, controlTags);

            return true;
        }

        protected void ShowFragment(string tag, int contentId, Bundle bundle, string controlTags, bool forceAddToBackStack = false, bool forceReplaceFragment = false)
        {
            IMvxCachedFragmentInfo fragInfo;
            FragmentCacheConfiguration.TryGetValue(tag, out fragInfo);

            IMvxCachedFragmentInfo currentFragInfo = null;
            var currentFragment = SupportFragmentManager.FindFragmentById(contentId);

            if (currentFragment != null)
                FragmentCacheConfiguration.TryGetValue(currentFragment.Tag, out currentFragInfo);

            if (fragInfo == null)
                throw new MvxException("Could not find tag: {0} in cache, you need to register it first.", tag);

            // We shouldn't replace the current fragment unless we really need to.
            FragmentReplaceMode fragmentReplaceMode = FragmentReplaceMode.ReplaceFragmentAndViewModel;
            if (!forceReplaceFragment)
                fragmentReplaceMode = ShouldReplaceCurrentFragment(fragInfo, currentFragInfo, bundle);

            if (fragmentReplaceMode == FragmentReplaceMode.NoReplace)
                return;

            var ft = SupportFragmentManager.BeginTransaction();
            OnBeforeFragmentChanging(fragInfo, ft);

            fragInfo.ContentId = contentId;

            //If we already have a previously created fragment, we only need to send the new parameters
            if (fragInfo.CachedFragment != null && fragmentReplaceMode == FragmentReplaceMode.ReplaceFragment)
            {
                (fragInfo.CachedFragment as Fragment)?.Arguments.Clear();
                (fragInfo.CachedFragment as Fragment)?.Arguments.PutAll(bundle);

                var childViewModelCache = Mvx.GetSingleton<IMvxChildViewModelCache>();
                var viewModelType = fragInfo.CachedFragment.ViewModel.GetType();
                if (childViewModelCache.Exists(viewModelType))
                {
                    fragInfo.CachedFragment.ViewModel = childViewModelCache.Get(viewModelType);
                    childViewModelCache.Remove(viewModelType);
                }
            }
            else
            {
                //Otherwise, create one and cache it
                fragInfo.CachedFragment = Fragment.Instantiate(this, FragmentJavaName(fragInfo.FragmentType),
                    bundle) as IMvxFragmentView;
                OnFragmentCreated(fragInfo, ft);
            }

            if (!string.IsNullOrEmpty(controlTags))
            {
                var elements = new List<string>();

                foreach (var controlTag in controlTags.Split('|'))
                {
                    View control = FindViewById(Android.Resource.Id.Content).FindViewWithTag(controlTag);

                    if (control == null)
                    {
                        Mvx.Warning($"No control with a tag \"{controlTag}\" was found on the current view.");
                        continue;
                    }
                    control.Tag = null;

                    var transitionName = ViewCompat.GetTransitionName(control);
                    if (string.IsNullOrEmpty(transitionName))
                    {
                        Mvx.Warning($"A XML {nameof(transitionName)} is required in order to transition a control when navigating.");
                        continue;
                    }

                    ft.AddSharedElement(control, transitionName);
                    elements.Add($"{controlTag}:{transitionName}");
                }

                (fragInfo.CachedFragment as Fragment)?.Arguments.PutString(DroidConstants.Transition_Name_Key, string.Join("|", elements));
            }

            currentFragment = fragInfo.CachedFragment as Fragment;
            ft.Replace(fragInfo.ContentId, fragInfo.CachedFragment as Fragment, fragInfo.Tag);

            //if replacing ViewModel then clear the cache after the fragment
            //has been added to the transaction so that the Tag property is not null
            //and the UniqueImmutableCacheTag property (if not overridden) has the correct value
            if (fragmentReplaceMode == FragmentReplaceMode.ReplaceFragmentAndViewModel)
            {
                var cache = Mvx.GetSingleton<IMvxMultipleViewModelCache>();
                cache.GetAndClear(fragInfo.ViewModelType, GetTagFromFragment(fragInfo.CachedFragment as Fragment));
            }

            if (currentFragment != null && fragInfo.AddToBackStack || forceAddToBackStack)
            {
                ft.AddToBackStack(fragInfo.Tag);
            }

            OnFragmentChanging(fragInfo, ft);
            ft.Commit();
            SupportFragmentManager.ExecutePendingTransactions();
            OnFragmentChanged(fragInfo);
        }
    }
}
