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
    public class Activity_Home : FragmentActivity
    {
        private ViewPager _viewPagerBanner;
        private RecyclerView _recyclerData;
        private ImageView _btnSearch, _btnMore, _btnShoppingCart;
        private Button _btnCategory1, _btnCategory2, _btnCategory3;
        private TextView _tvName;
        private Android.Support.V4.Widget.DrawerLayout _drawerLayout;
        private PagerAdapter _viewPagerBannerAdapter;
        private List<BeanMonAn> _listMonAn = new List<BeanMonAn>();
        private string _JsonResult_ListMonAn = "";
        private int _countPager = 0;
        private System.Timers.Timer Timer1 = new System.Timers.Timer();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            SetData();
        }

        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Home);
            _btnSearch = FindViewById<ImageView>(Resource.Id.Home_btnSearch);
            _btnMore = FindViewById<ImageView>(Resource.Id.Home_btnMore);
            _btnShoppingCart = FindViewById<ImageView>(Resource.Id.Home_btnCart);
            _btnCategory1 = FindViewById<Button>(Resource.Id.Home_btnCategory1);
            _btnCategory2 = FindViewById<Button>(Resource.Id.Home_btnCategory2);
            _btnCategory3 = FindViewById<Button>(Resource.Id.Home_btnCategory3);
            _tvName = FindViewById<TextView>(Resource.Id.Home_tvName);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.Home_RecyclerView_Data);
            _viewPagerBanner = FindViewById<ViewPager>(Resource.Id.Home_ViewPagerBanner);
            HomeViewPagerBannerAdapter adapter = new HomeViewPagerBannerAdapter(SupportFragmentManager);
            _viewPagerBanner.Adapter = adapter;
            SetupTimerViewPager();
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
                List<BeanSession> list = new List<BeanSession>();
                list = SQLiteDataHandler.BeanSession_LoadList();
                if (list != null && list.Count > 0) // Co session
                {
                    Intent intent = new Intent(this, typeof(Activity_Logout));
                    StartActivity(intent);
                }
                else // chua co session
                {
                    Intent intent = new Intent(this, typeof(Activity_Login2));
                    StartActivity(intent);
                }
            };
            _btnShoppingCart.Click += delegate
            {
                List<BeanSession> list = new List<BeanSession>();
                list = SQLiteDataHandler.BeanSession_LoadList();
                if (list != null && list.Count > 0) // Co session
                {
                    Intent intent = new Intent(this, typeof(Activity_Bill_Confirm));
                    StartActivity(intent);
                }
                else // chua co session
                {
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Lưu ý");
                    alert.SetMessage("Vui lòng đăng nhập vào hệ thống!");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        dialog.Dispose();
                    });
                    alert.Show();
                }
            };

        }
        private void Click_RecyclerData(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(Activity_Food_Detail));
            intent.PutExtra("Detail_MaMon", _listMonAn[e].MaMon.ToString());
            StartActivity(intent);
        }
        private void SetData()
        {
            try
            {
                // check session
                List<BeanSession> list = new List<BeanSession>();
                list = SQLiteDataHandler.BeanSession_LoadList();
                if (list != null && list.Count > 0) // Co session
                {
                    _tvName.Text = list[0].TaiKhoan;
                }
                else // chua co session
                {
                    _tvName.Text = "Bạn chưa đăng nhập!";
                }

                _listMonAn = SQLiteDataHandler.BeanMonAn_LoadList();
                if (_listMonAn != null && _listMonAn.Count > 0)
                {
                    HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn, 0);
                    adapter.ItemClick += Click_RecyclerData;
                    _recyclerData.SetAdapter(adapter);
                    _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        private void SetupTimerViewPager()
        {
            //Timer1.Interval = 10000;
            //Timer1.Enabled = true;
            //Timer1.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            //{
            //    if (_countPager == 0)
            //    {
            //        _countPager++;
            //        _viewPagerBanner.SetCurrentItem(_countPager, true);
            //    }
            //    else if (_countPager == 1)
            //    {
            //        _countPager++;
            //        _viewPagerBanner.SetCurrentItem(_countPager, true);
            //    }
            //    else if (_countPager == 2)
            //    {
            //        _countPager = 0;
            //        _viewPagerBanner.SetCurrentItem(_countPager, true);
            //    }

            //};
            //Timer1.Start();
        }

    }
}