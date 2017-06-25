using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Placeholder.SharedElement.Core.ViewModels;

namespace Placeholder.SharedElement.Droid.Views
{
    [Activity(
        Theme = "@style/AppTheme",
        Name = DroidConstants.SharedElement_Views_Namespace + nameof(DetailsActivity))]
    public class DetailsActivity : MvxAppCompatActivity<DetailsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_details);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SetTitle(Resource.String.app_project_name);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
