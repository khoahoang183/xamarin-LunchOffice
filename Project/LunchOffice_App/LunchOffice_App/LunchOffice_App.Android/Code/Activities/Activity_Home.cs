using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using LunchOffice_App.Droid.Activities;
using LunchOffice_App.Droid.Code.Adapter;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.SQLite;
using LunchOffice_App.Droid.Code.Utilities;
using static Android.Resource;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Home", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Home : Activity
    {
        ViewPager _viewPagerBanner;
        RecyclerView _recyclerData;
        Button _btnSearch, _btnMore;
        Button _btnCategory1, _btnCategory2, _btnCategory3;
        private Android.Support.V4.Widget.DrawerLayout _drawerLayout;
        private PagerAdapter _viewPagerBannerAdapter;
        List<BeanMonAn> _listMonAn = new List<BeanMonAn>();
        string _JsonResult_ListMonAn = "";


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            if (Intent != null)
            {
                _JsonResult_ListMonAn = Intent.GetStringExtra("JsonResult_ListMonAn");

                Toast.MakeText(this, _JsonResult_ListMonAn, ToastLength.Long).Show();

            }

        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Home);
            _btnSearch = FindViewById<Button>(Resource.Id.Home_btnSearch);
            _btnMore = FindViewById<Button>(Resource.Id.Home_btnMore);
            _btnCategory1 = FindViewById<Button>(Resource.Id.Home_btnCategory1);
            _btnCategory2 = FindViewById<Button>(Resource.Id.Home_btnCategory2);
            _btnCategory3 = FindViewById<Button>(Resource.Id.Home_btnCategory3);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.Home_RecyclerView_Data);
            _viewPagerBanner = FindViewById<ViewPager>(Resource.Id.Home_ViewPagerBanner);
            _viewPagerBanner.SetBackgroundResource(Resource.Drawable.img_banner001);


            setupData();
            HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn, 0);
            _recyclerData.SetAdapter(adapter);
            _recyclerData.SetLayoutManager(new LinearLayoutManager(this));

            _btnCategory1.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Food_Category));
                intent.PutExtra("Category", "1");
                StartActivity(intent);

            };
            _btnCategory2.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Food_Category));
                intent.PutExtra("Category", "2");
                StartActivity(intent);
            };
            _btnCategory3.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Food_Category));
                intent.PutExtra("Category", "3");
                StartActivity(intent);
            };
            _btnSearch.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Food_Category));
                intent.PutExtra("Category", "0");
                StartActivity(intent);
            };
            _btnMore.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Login));
                StartActivity(intent);
            };

        }


        private void setupData()
        {
            _listMonAn = SQLiteDataHandler.BeanMonAn_LoadList();
        }


    }
}