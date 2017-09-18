using System.Collections.Generic;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Support.V4.Util;
using Android.Support.V4.View;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Shared.Presenter;
using MvvmCross.Platform;
using Placeholder.SharedElement.Core;

namespace Placeholder.SharedElement.Droid.Views
{
    public class SharedElementFragmentsPresenter : MvxFragmentsPresenter
    {
        public SharedElementFragmentsPresenter(IEnumerable<Assembly> AndroidViewAssemblies)
            : base(AndroidViewAssemblies)
        {
        }

        protected override void ShowActivity(MvxViewModelRequest request, MvxViewModelRequest fragmentRequest = null)
        {
            if (InterceptPresenter(request))
                return;

            Show(request, fragmentRequest);
        }

        private bool InterceptPresenter(MvxViewModelRequest request)
        {
            if ((request.PresentationValues?.ContainsKey(SharedConstants.Animate_Tag) ?? false)
                && request.PresentationValues.TryGetValue(SharedConstants.Animate_Tag, out var controlTags))
            {
                Pair[] GetTransitionControls(string tags)
                {
                    string[] tagArray = tags.Split('|');
                    var transitionElementPairs = new List<Pair>();

                    foreach (var tag in tagArray)
                    {
                        View control = Activity.FindViewById(Android.Resource.Id.Content).FindViewWithTag(tag);

                        if (control == null)
                        {
                            Mvx.Warning($"No control with a tag \"{tag}\" was found on the current view.");
                            continue;
                        }
                        control.Tag = null;

                        var transitionName = ViewCompat.GetTransitionName(control);
                        if (string.IsNullOrEmpty(transitionName))
                        {
                            Mvx.Warning($"A XML {nameof(transitionName)} is required in order to transition a control when navigating.");
                            continue;
                        }

                        transitionElementPairs.Add(Pair.Create(control, transitionName));
                    }

                    return transitionElementPairs.ToArray();
                }

                Pair[] controlTransitionNamePairs = GetTransitionControls(controlTags);

                if (controlTransitionNamePairs.Length == 0)
                {
                    Mvx.Warning("No transition elements found, navigating via base MvvmCross navigation.");
                    return false;
                }

                var activityOptions = ActivityOptionsCompat.MakeSceneTransitionAnimation(Activity, controlTransitionNamePairs);
                Intent intent = CreateIntentForRequest(request);

                Activity.StartActivity(intent, activityOptions.ToBundle());
                return true;
            }

            return false;
        }
    }
}
