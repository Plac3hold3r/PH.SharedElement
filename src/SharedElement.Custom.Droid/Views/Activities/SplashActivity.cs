using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace SharedElement.Custom.Droid.Views
{
    [Activity(
        NoHistory = true,
        MainLauncher = true,
        Label = "@string/app_name",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/AppTheme.Splash",
        RoundIcon = "@mipmap/ic_round_launcher",
        ScreenOrientation = ScreenOrientation.Portrait,
        Name = DroidConstants.SharedElement_Views_Namespace + nameof(SplashActivity))]
    public class SplashActivity : MvxSplashScreenAppCompatActivity
    {
        public SplashActivity() : base(Resource.Layout.activity_splash_screen)
        {
        }
    }
}
