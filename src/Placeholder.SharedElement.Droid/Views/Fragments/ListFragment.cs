using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using Placeholder.SharedElement.Core.ViewModels;
using Placeholder.SharedElement.Droid.Adapters;

namespace Placeholder.SharedElement.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register(DroidConstants.SharedElement_Views_Namespace + nameof(ListFragment))]
    public class ListFragment : MvxFragment<ListViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_list, null);

            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);
            if (recyclerView != null)
            {
                recyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                recyclerView.SetLayoutManager(layoutManager);

                var adapter = new SelectedItemRecyclerAdapter(BindingContext as IMvxAndroidBindingContext);
                adapter.OnItemClick += Adapter_OnItemClick;
                recyclerView.Adapter = adapter;
            }

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            (Activity as AppCompatActivity)?.SupportActionBar.SetDisplayHomeAsUpEnabled(false);
        }

        private void Adapter_OnItemClick(object sender, SelectedItemRecyclerAdapter.SelectedItemEventArgs e)
        {
            Toast.MakeText(Activity, $"Selected item {e.Position + 1}", ToastLength.Short)
                .Show();

            var itemLogo = e.View.FindViewById<ImageView>(Resource.Id.img_logo);
            itemLogo.Tag = DroidConstants.Transform_Animate_Image_Tag;

            var itemName = e.View.FindViewById<TextView>(Resource.Id.txt_name);
            itemName.Tag = DroidConstants.Transform_Animate_Text_Tag;

            ViewModel.SelectItemExecution(e.DataContext as ListItemViewModel, itemLogo.Tag.ToString(), itemName.Tag.ToString());
        }
    }
}
