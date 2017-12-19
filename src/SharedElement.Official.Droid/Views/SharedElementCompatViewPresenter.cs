using Android.App;
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
using SharedElement.Official.Core;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharedElement.Official.Droid.Views
{
    public class SharedElementCompatViewPresenter : MvxAppCompatViewPresenter
    {
        protected readonly IMvxLog Log;

        public SharedElementCompatViewPresenter(IEnumerable<Assembly> androidViewAssemblies) : base(androidViewAssemblies)
        {
            Log = Mvx.Resolve<IMvxLogProvider>().GetLogFor<SharedElementCompatViewPresenter>();
        }

        protected override void ShowActivity(Type view,
             MvxActivityPresentationAttribute attribute,
             MvxViewModelRequest request)
        {
            Intent intent = CreateIntentForRequest(request);
            if (attribute.Extras != null)
                intent.PutExtras(attribute.Extras);

            Activity activity = CurrentActivity;
            if (activity == null)
            {
                Log.Warn("Cannot Resolve current top activity");
                return;
            }

            if ((request.PresentationValues?.ContainsKey(SharedConstants.Animate_Tag) ?? false)
                && request.PresentationValues.TryGetValue(SharedConstants.Animate_Tag, out string controlTags))
            {
                (List<string> Elements, Pair[] Pairs) = GetTransitionControls(activity, controlTags);

                if (Pairs.Length != 0)
                {
                    var activityOptions = ActivityOptionsCompat.MakeSceneTransitionAnimation(activity, Pairs);
                    intent.PutExtra(DroidConstants.Transition_Name_Key, string.Join("|", Elements));

                    activity.StartActivity(intent, activityOptions.ToBundle());
                    return;
                }
                else
                {
                    Log.Warn("No transition elements found, navigating via base MvvmCross navigation.");
                }
            }

            activity.StartActivity(intent);
        }

        protected virtual (List<string> Elements, Pair[] Pairs) GetTransitionControls(Activity activity, string tags)
        {
            string[] tagArray = tags.Split('|');
            var transitionElementPairs = new List<Pair>();
            var elements = new List<string>();

            foreach (var tag in tagArray)
            {
                View control = activity.FindViewById(Android.Resource.Id.Content).FindViewWithTag(tag);

                if (control == null)
                {
                    Log.Warn($"No control with a tag \"{tag}\" was found on the current view.");
                    continue;
                }
                control.Tag = null;

                var transitionName = ViewCompat.GetTransitionName(control);
                if (string.IsNullOrEmpty(transitionName))
                {
                    Log.Warn($"A XML {nameof(transitionName)} is required in order to transition a control when navigating.");
                    continue;
                }

                transitionElementPairs.Add(Pair.Create(control, transitionName));
                elements.Add($"{tag}:{transitionName}");
            }

            return (elements, transitionElementPairs.ToArray());
        }

        protected override void PerformShowFragmentTransaction(
            Android.Support.V4.App.FragmentManager fragmentManager,
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

            ft.Replace(attribute.FragmentContentId, (Android.Support.V4.App.Fragment)fragment, fragmentName);
            ft.CommitAllowingStateLoss();

            OnFragmentChanged(ft, fragmentView, attribute);
        }

        protected virtual void OnBeforeFragmentChanging(
            Android.Support.V4.App.FragmentTransaction ft,
            Android.Support.V4.App.Fragment fragmentView,
            MvxFragmentPresentationAttribute attribute,
            MvxViewModelRequest request)
        {
            var controlTags = string.Empty;
            request.PresentationValues?.TryGetValue(SharedConstants.Animate_Tag, out controlTags);

            if (!string.IsNullOrEmpty(controlTags))
            {
                var elements = new List<string>();

                foreach (var controlTag in controlTags.Split('|'))
                {
                    View control = CurrentActivity.FindViewById(Android.Resource.Id.Content).FindViewWithTag(controlTag);

                    if (control == null)
                    {
                        Log.Warn("No control with a tag \"{0}\" was found on the current view.", controlTag);
                        continue;
                    }
                    control.Tag = null;

                    var transitionName = ViewCompat.GetTransitionName(control);
                    if (string.IsNullOrEmpty(transitionName))
                    {
                        Log.Warn("A XML {0} is required in order to transition a control when navigating.", nameof(transitionName));
                        continue;
                    }

                    ft.AddSharedElement(control, transitionName);
                    elements.Add($"{controlTag}:{transitionName}");
                }

                fragmentView.Arguments.PutString(DroidConstants.Transition_Name_Key, string.Join("|", elements));
            }

            OnBeforeFragmentChanging(ft, fragmentView, attribute);
        }
    }
}
