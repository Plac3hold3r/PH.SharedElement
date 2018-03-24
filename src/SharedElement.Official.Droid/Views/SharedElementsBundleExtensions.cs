using Android.OS;
using Android.Views;
using System.Collections.Generic;

namespace SharedElement.Official.Droid.Views
{
    public static class SharedElementsBundleExtensions
    {
        public static IDictionary<string, string> GetSharedElementTransitionNames(this Bundle bundle)
        {
            IDictionary<string, string> sharedElements = new Dictionary<string, string>();
            var transitions = bundle.GetString(DroidConstants.Transition_Name_Key);

            foreach (var transition in transitions.Split('|'))
            {
                string[] transitionDetails = transition.Split(':');
                sharedElements.Add(new KeyValuePair<string, string>(transitionDetails[0], transitionDetails[1]));
            }

            return sharedElements;
        }

        public static void SetSharedElementsByTag(this Bundle bundle, View view)
        {
            var transitions = bundle.GetString(DroidConstants.Transition_Name_Key);

            foreach (var transition in transitions.Split('|'))
            {
                string[] transitionDetails = transition.Split(':');
                View viewToAnimate = view.FindViewWithTag(transitionDetails[0]);
                viewToAnimate.SetTransitionNameSupport(transitionDetails[1]);
            }
        }

        public static void SetSharedElementsById(this Bundle bundle, View view)
        {
            var transitions = bundle.GetString(DroidConstants.Transition_Name_Key);

            foreach (var transition in transitions.Split('|'))
            {
                string[] transitionDetails = transition.Split(':');
                View viewToAnimate = view.FindViewById(view.Context.Resources.GetIdentifier(transitionDetails[0], "id", view.Context.PackageName));
                viewToAnimate.SetTransitionNameSupport(transitionDetails[1]);
            }
        }

        public static void SetTransitionNameSupport(this View view, string transitionName)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                view.TransitionName = transitionName;
        }
    }
}
