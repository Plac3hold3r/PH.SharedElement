using System;
using Android.Views;

namespace SharedElement.Presenter.Droid.Adapters
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
