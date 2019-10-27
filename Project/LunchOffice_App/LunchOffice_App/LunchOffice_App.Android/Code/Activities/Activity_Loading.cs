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

        #region Result From API
        public static List<BeanMonAn> API_RESULT_LISTMONAN = new List<BeanMonAn>();
        #endregion
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            requestPermission();
            getLayout();
            Task.Run(async () =>
            {
                getAllAPIData();
                setupSQLite();
                Finish();
                Intent intent = new Intent(this, typeof(Activity_Home));
                StartActivity(intent);
            });

        }

        private void getAllAPIData()
        {
            try
            {
                Task.Run(async () =>
                {
                    //API_RESULT_LISTMONAN = Utilities_API.API_GetListMonAn();
                });
            }
            catch (Exception ex)
            {

            }

        }

        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Loading);
        }

        private void setupSQLite()
        {
            SetupTestData(); // de test
            SQLiteDataHandler.CreateDBSQLite();
            SQLiteDataHandler.BeanMonAn_ClearList();
            foreach (BeanMonAn item in API_RESULT_LISTMONAN)
            {
                SQLiteDataHandler.BeanMonAn_Insert(item);
            }

        }

        private void checkLogin()
        {

        }
        private void SetupTestData()
        {
            BeanMonAn item1 = new BeanMonAn(001, "Cơm Chiên Dương Châu", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 50000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item2 = new BeanMonAn(002, "Cơm Sườn Nướng", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 120000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item3 = new BeanMonAn(003, "Cơm Cá Hú Kho Tộ", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 75000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item4 = new BeanMonAn(004, "Cơm Sườn Xào Chua Ngọt", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 60000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item5 = new BeanMonAn(005, "Cơm Canh Chua Cá Hú", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 85000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item6 = new BeanMonAn(006, "Cơm Mắm Chưng", 1, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 30000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item7 = new BeanMonAn(007, "Bùn Bò Huế", 2, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 100000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item8 = new BeanMonAn(008, "Phở Bò", 2, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 64000, 1, DateTime.Now, DateTime.Now, "", "");
            BeanMonAn item9 = new BeanMonAn(009, "Nước Suối", 3, "Mô tả", "https://www.gulfshorelife.com/wp-content/uploads/2019/07/Jul19_Appetite1-256x256.jpg", 10000, 1, DateTime.Now, DateTime.Now, "", "");
            API_RESULT_LISTMONAN.Add(item1); API_RESULT_LISTMONAN.Add(item2); API_RESULT_LISTMONAN.Add(item3); API_RESULT_LISTMONAN.Add(item4); API_RESULT_LISTMONAN.Add(item5);
            API_RESULT_LISTMONAN.Add(item6); API_RESULT_LISTMONAN.Add(item7); API_RESULT_LISTMONAN.Add(item8); API_RESULT_LISTMONAN.Add(item9);
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