using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Activities;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Lunch Office", Icon = "@drawable/ic_logo_square", ScreenOrientation = ScreenOrientation.Portrait,
        Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class Activity_Loading : Activity
    {
        int REQUEST_PERMISSION_CODE = 1;
        bool IS_GRANTED_PERMISSION = false;
        string[] _permissionList = new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            requestPermission();
            getLayout();
            System.Threading.Thread.Sleep(5000);
            Intent intent = new Intent(this, typeof(Activity_Login));
            this.Finish();
            StartActivity(intent);
        }

        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Loading);
        }

        private void checkLogin()
        {
            
        }

        private void checkSQLite()
        {

        }

        #region permission
        private void requestPermission()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == Permission.Denied
                && ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(this, _permissionList, REQUEST_PERMISSION_CODE);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case 1:
                    {
                        // If request is cancelled, the result arrays are empty.
                        if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                        {
                            IS_GRANTED_PERMISSION = true;
                            return;
                        }
                        else
                        {
                            ActivityCompat.RequestPermissions(this, _permissionList, REQUEST_PERMISSION_CODE);
                        }
                    }
                    break;
            }
        }

        #endregion
    }
}