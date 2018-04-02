using System.Collections.Generic;
using Android.Views;
using MvvmCross.Core.ViewModels;

namespace SharedElement.Presenter.Droid.Views
{
    /// <summary>
    /// Used by Android presenters to check if they need to include shared element animations on navigation
    /// </summary>
    public interface IMvxAndroidSharedElements
    {
        /// <summary>
        /// Fetches views to add to the shared elements transition.
        /// </summary>
        /// <param name="request">The <see cref="MvxViewModelRequest"/> used for the navigation.</param>
        /// <returns>An <see cref="IDictionary{key, value}"/> containing the identifier key and view to animate with assigned transition name.</returns>
        IDictionary<string, View> FetchSharedElementsToAnimate(MvxViewModelRequest request);
    }
}
