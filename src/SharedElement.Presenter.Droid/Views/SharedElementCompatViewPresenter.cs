using System;
using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Util;
using Android.Support.V4.View;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Views;
using MvvmCross.Droid.Views.Attributes;
using MvvmCross.Platform;
using MvvmCross.Platform.Logging;

namespace SharedElement.Presenter.Droid.Views
{
    public class SharedElementCompatViewPresenter : MvxAppCompatViewPresenter
    {
        protected readonly IMvxLog Log;

        public SharedElementCompatViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
            Log = Mvx.Resolve<IMvxLogProvider>().GetLogFor<SharedElementCompatViewPresenter>();
        }

        protected override void PerformShowFragmentTransaction(
            FragmentManager fragmentManager,
            MvxFragmentPresentationAttribute attribute,
            MvxViewModelRequest request)
        {
            var fragmentName = FragmentJavaName(attribute.ViewType);

            IMvxFragmentView fragment = null;
            if (attribute.IsCacheableFragment)
            {
                fragment = (IMvxFragmentView)fragmentManager.FindFragmentByTag(fragmentName);
            }
            fragment = fragment ?? CreateFragment(attribute, fragmentName);

            var fragmentView = fragment.ToFragment();

            // MvxNavigationService provides an already instantiated ViewModel here
            if (request is MvxViewModelInstanceRequest instanceRequest)
            {
                fragment.ViewModel = instanceRequest.ViewModelInstance;
            }

            // save MvxViewModelRequest in the Fragment's Arguments
            var bundle = new Bundle();
            var serializedRequest = NavigationSerializer.Serializer.SerializeObject(request);
            bundle.PutString(ViewModelRequestBundleKey, serializedRequest);

            if (fragmentView != null)
            {
                if (fragmentView.Arguments == null)
                {
                    fragmentView.Arguments = bundle;
                }
                else
                {
                    fragmentView.Arguments.Clear();
                    fragmentView.Arguments.PutAll(bundle);
                }
            }

            var ft = fragmentManager.BeginTransaction();

            OnBeforeFragmentChanging(ft, fragmentView, attribute, request);

            if (attribute.AddToBackStack == true)
                ft.AddToBackStack(fragmentName);

            OnFragmentChanging(ft, fragmentView, attribute);

            ft.Replace(attribute.FragmentContentId, (Fragment)fragment, fragmentName);
            ft.CommitAllowingStateLoss();

            OnFragmentChanged(ft, fragmentView, attribute);
        }

        protected virtual void OnBeforeFragmentChanging(FragmentTransaction ft, Fragment fragment, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest request)
        {
            if (CurrentActivity is IMvxAndroidSharedElements sharedElementsActivity)
            {
                var elements = new List<string>();

                foreach (KeyValuePair<string, View> item in sharedElementsActivity.FetchSharedElementsToAnimate(request))
                {
                    var transitionName = ViewCompat.GetTransitionName(item.Value);
                    if (string.IsNullOrEmpty(transitionName))
                    {
                        Log.Warn("A XML {0} is required in order to transition a control when navigating.", nameof(transitionName));
                        continue;
                    }

                    ft.AddSharedElement(item.Value, transitionName);
                    elements.Add($"{item.Key}:{transitionName}");
                }

                if (elements.Count > 0)
                    fragment.Arguments.PutString(DroidConstants.Transition_Name_Key, string.Join("|", elements));
            }

            if (!attribute.EnterAnimation.Equals(int.MinValue) && !attribute.ExitAnimation.Equals(int.MinValue))
            {
                if (!attribute.PopEnterAnimation.Equals(int.MinValue) && !attribute.PopExitAnimation.Equals(int.MinValue))
                    ft.SetCustomAnimations(attribute.EnterAnimation, attribute.ExitAnimation, attribute.PopEnterAnimation, attribute.PopExitAnimation);
                else
                    ft.SetCustomAnimations(attribute.EnterAnimation, attribute.ExitAnimation);
            }

            if (attribute.TransitionStyle != int.MinValue)
                ft.SetTransitionStyle(attribute.TransitionStyle);
        }

        protected override void ShowActivity(Type view,
             MvxActivityPresentationAttribute attribute,
             MvxViewModelRequest request)
        {
            Intent intent = CreateIntentForRequest(request);
            if (attribute.Extras != null)
                intent.PutExtras(attribute.Extras);

            var activity = CurrentActivity;
            if (activity == null)
            {
                Log.Warn("Cannot Resolve current top activity");
                return;
            }

            if (CurrentActivity is IMvxAndroidSharedElements sharedElementsActivity)
            {
                var elements = new List<string>();
                var transitionElementPairs = new List<Pair>();

                foreach (KeyValuePair<string, View> item in sharedElementsActivity.FetchSharedElementsToAnimate(request))
                {
                    var transitionName = ViewCompat.GetTransitionName(item.Value);
                    if (string.IsNullOrEmpty(transitionName))
                    {
                        Log.Warn("A XML {0} is required in order to transition a control when navigating.", nameof(transitionName));
                        continue;
                    }

                    transitionElementPairs.Add(Pair.Create(item.Value, transitionName));
                    elements.Add($"{item.Key}:{transitionName}");
                }

                if (elements.Count > 0)
                {
                    var activityOptions = ActivityOptionsCompat.MakeSceneTransitionAnimation(activity, transitionElementPairs.ToArray());
                    intent.PutExtra(DroidConstants.Transition_Name_Key, string.Join("|", elements));
                    activity.StartActivity(intent, activityOptions.ToBundle());
                    return;
                }
            }

            activity.StartActivity(intent);
        }
    }
}
