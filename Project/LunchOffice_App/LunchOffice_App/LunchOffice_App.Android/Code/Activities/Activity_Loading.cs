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
using LunchOffice_App.Droid.Code.Utilities;
using LunchOffice_App.Droid.Activities;
using LunchOffice_App.Droid.Code.Bean;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LunchOffice_App.Droid.Code.SQLite;
using LunchOffice_App.Droid.Code.Cmm;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Lunch Office", Icon = "@drawable/ic_logo_square", ScreenOrientation = ScreenOrientation.Portrait,
        Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class Activity_Loading : Activity
    {
        #region Permission
        string[] _permissionList = new string[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage };
        int REQUEST_PERMISSION_CODE = 1;
        #endregion
        private ProgressBar _pro;
        #region Result From API
        public static List<BeanMonAn> API_RESULT_LISTMONAN = new List<BeanMonAn>();
        #endregion
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            requestPermission();
            getLayout();
            // get List Mon An
            await Utilities_API.API_GetListMonAn();
            API_RESULT_LISTMONAN = Utilities_API._lstMonAn;

            setupSQLite();
            Finish();
            Intent intent = new Intent(this, typeof(Activity_Home));
            StartActivity(intent);
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Loading);
            _pro = FindViewById<ProgressBar>(Resource.Id.progressDownload);
            _pro.Visibility = ViewStates.Visible;
            _pro.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(Color.Red);
        }
        private void setupSQLite()
        {
            SQLiteDataHandler.CreateDBSQLite();
            if (API_RESULT_LISTMONAN != null && API_RESULT_LISTMONAN.Count > 0)
            {
                List<BeanMonAn> listMonAn = new List<BeanMonAn>();
                listMonAn = SQLiteDataHandler.BeanMonAn_LoadList();
                foreach (BeanMonAn item in API_RESULT_LISTMONAN)
                {
                    if (String.IsNullOrEmpty(item.SearchData)) // de search
                    {
                        item.SearchData = CmmFunction.RemoveVietNamAccent(item.TenMon) + " " + CmmFunction.RemoveVietNamAccent(item.MieuTa);
                    }
                    if (listMonAn.Any(x => x.MaMon.Equals(item.MaMon))) // da ton tai trong db
                    {
                        BeanMonAn temp = listMonAn.Find(x => x.MaMon.Equals(item.MaMon));
                        if (temp.Modified != item.Modified) // cap nhat moi
                        {
                            SQLiteDataHandler.BeanMonAn_Update(item);
                            Utilities_DownloadImageFromURL download = new Utilities_DownloadImageFromURL(this);
                            string url = Utilities_API._SiteName + Utilities_API._SiteImageUrl + item.HinhAnh;
                            download.Execute(url);
                        }
                    }
                    else
                    {
                        SQLiteDataHandler.BeanMonAn_Insert(item);
                        Utilities_DownloadImageFromURL download = new Utilities_DownloadImageFromURL(this);
                        string url = Utilities_API._SiteName + Utilities_API._SiteImageUrl + item.HinhAnh;
                        download.Execute(url);
                    }
                }
            }
        }

        #region Permission
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