using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using SharedElement.Official.Core.ViewModels;

namespace SharedElement.Official.Droid.Views
{
    [Activity(
        Theme = "@style/AppTheme",
        Name = DroidConstants.SharedElement_Views_Namespace + nameof(MainActivity))]
    public class MainActivity : MvxAppCompatActivity<MainViewModel>, IMvxAndroidSharedElements
    {
        public IDictionary<string, View> FetchSharedElementsToAnimate(MvxBasePresentationAttribute attribute, MvxViewModelRequest request)
        {
            IDictionary<string, View> sharedElements = new Dictionary<string, View>();

            KeyValuePair<string, View>? iconAnim = CreateSharedElementPair(Resource.String.transition_list_item_icon);
            if (iconAnim != null)
                sharedElements.Add(iconAnim.GetValueOrDefault());

            KeyValuePair<string, View>? nameAnim = CreateSharedElementPair(Resource.String.transition_list_item_name);
            if (nameAnim != null)
                sharedElements.Add(nameAnim.GetValueOrDefault());

            return sharedElements;
        }

        private KeyValuePair<string, View>? CreateSharedElementPair(int tagStringResourceId)
        {
            var controlTag = Resources.GetString(tagStringResourceId);
            View control = FindViewById(Android.Resource.Id.Content).FindViewWithTag(controlTag);
            if (control != null)
            {
                control.Tag = null;
                return new KeyValuePair<string, View>(controlTag, control);
            }

            return null;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_main);
            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SetTitle(Resource.String.app_project_name);

            if (bundle is null)
                ViewModel.Test();
        }
    }
}
