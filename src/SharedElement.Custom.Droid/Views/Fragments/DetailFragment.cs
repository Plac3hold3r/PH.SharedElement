using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Transitions;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using SharedElement.Custom.Core.ViewModels;

namespace SharedElement.Custom.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register(DroidConstants.SharedElement_Views_Namespace + nameof(DetailFragment))]
    public class DetailFragment : MvxFragment<DetailFragmentViewModel>
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                SharedElementEnterTransition = TransitionInflater.From(Activity).InflateTransition(Android.Resource.Transition.Move);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;

            base.OnCreateView(inflater, container, savedInstanceState);

            View view = this.BindingInflate(Resource.Layout.fragment_details, null);
            var transitions = Arguments.GetString(DroidConstants.Transition_Name_Key);

            foreach (var transition in transitions.Split('|'))
            {
                string[] transitionDetails = transition.Split(':');

                View viewToAnimate = view.FindViewWithTag(transitionDetails[0]);
                ViewCompat.SetTransitionName(viewToAnimate, transitionDetails[1]);
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            (Activity as AppCompatActivity)?.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Activity.OnBackPressed();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
