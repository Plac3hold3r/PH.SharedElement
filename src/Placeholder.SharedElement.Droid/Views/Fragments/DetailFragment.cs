using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using Placeholder.SharedElement.Core.ViewModels;

namespace Placeholder.SharedElement.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register(DroidConstants.SharedElement_Views_Namespace + nameof(DetailFragment))]
    public class DetailFragment : MvxFragment<DetailFragmentViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            return this.BindingInflate(Resource.Layout.fragment_details, null);
        }
    }
}
