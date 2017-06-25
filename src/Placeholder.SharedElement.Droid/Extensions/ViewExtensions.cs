using Android.OS;
using Android.Views;

namespace Placeholder.SharedElement.Droid.Extensions
{
    public static class ViewExtensions
    {
        public static string GetTransitionNameSupport(this View view)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                return view.TransitionName;

            return string.Empty;
        }
    }
}
