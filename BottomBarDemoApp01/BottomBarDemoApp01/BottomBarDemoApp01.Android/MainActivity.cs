using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Color = Android.Graphics.Color;

namespace BottomBarDemoApp01.Droid
{
    [Activity(Label = "BottomBarDemoApp01", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //sets the status bar color
            if ((Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop))
            {
                Window.SetStatusBarColor(Color.Transparent);
                Window.SetNavigationBarColor(Color.Transparent);
                Window.SetBackgroundDrawableResource(Resource.Drawable.gradient);
            }

            LoadApplication(new App());
        }
    }
}

