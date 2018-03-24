using Android.Views;
using MvvmCross.Core.ViewModels;
using System.Collections.Generic;

namespace SharedElement.Official.Droid.Views
{
    public interface IMvxAndroidSharedElements
    {
        IDictionary<string, View> FetchSharedElementsToAnimate(MvxViewModelRequest request);
    }
}
