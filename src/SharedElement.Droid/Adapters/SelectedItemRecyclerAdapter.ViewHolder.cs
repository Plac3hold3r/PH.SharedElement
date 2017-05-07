using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System;

namespace SharedElement.Droid.Adapters
{
    public partial class SelectedItemRecyclerAdapter
    {
        public class SelectedItemViewHolder : MvxRecyclerViewHolder
        {
            readonly Action<int, View, object> _listener;

            public SelectedItemViewHolder(View itemView, IMvxAndroidBindingContext context, Action<int, View, object> listener)
                : base(itemView, context)
            {
                _listener = listener;
                ItemView.Click += ItemView_Click;
            }

            private void ItemView_Click(object sender, EventArgs e)
            {
                _listener(AdapterPosition, ItemView, DataContext);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                    ItemView.Click -= ItemView_Click;

                base.Dispose(disposing);
            }
        }
    }
}
