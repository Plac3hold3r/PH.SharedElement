using Android.Views;
using System;

namespace SharedElement.Custom.Droid.Adapters
{
    public partial class SelectedItemRecyclerAdapter
    {
        public class SelectedItemEventArgs : EventArgs
        {
            public SelectedItemEventArgs(int position, View view, object dataContext)
            {
                Position = position;
                View = view;
                DataContext = dataContext;
            }

            public int Position { get; }
            public View View { get; }
            public object DataContext { get; }
        }
    }
}
