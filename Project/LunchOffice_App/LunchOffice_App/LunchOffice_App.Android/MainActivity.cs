using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LunchOffice_App.Droid.Code.Utilities;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;

namespace LunchOffice_App.Droid
{
    /*
    [Activity(Label = "LunchOffice_App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait,
                MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
                */
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  

            var perms = new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage };
            ActivityCompat.RequestPermissions(this, perms, 0);


            SetContentView(Resource.Layout.layouttest);
            var btnDownload = FindViewById<Button>(Resource.Id.btnDownload);
            var imageView = FindViewById<ImageView>(Resource.Id.imageView);
            btnDownload.Click += delegate
            {
                Utilities_DownloadImageFromURL download = new Utilities_DownloadImageFromURL(this, imageView);
                download.Execute("https://images.alphacoders.com/911/911335.jpg");

            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }


}
