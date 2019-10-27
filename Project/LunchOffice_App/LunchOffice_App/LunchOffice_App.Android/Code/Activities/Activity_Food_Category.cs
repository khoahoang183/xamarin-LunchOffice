using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using LunchOffice_App.Droid.Code.Adapter;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.SQLite;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Food_Category", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Food_Category : Activity
    {
        EditText _edtSearch;
        Button _btnCancel;
        ImageView _imgDeleteText;
        RecyclerView _recyclerData;
        int _category = 0;
        List<BeanMonAn> _listMonAn = new List<BeanMonAn>();
        List<BeanMonAn> _listMonAn_Filtered = new List<BeanMonAn>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            // Create your application here
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Food_Category);
            if (Intent != null)
            {
                _category = int.Parse(Intent.GetStringExtra("Category"));
            }
            _edtSearch = FindViewById<EditText>(Resource.Id.FoodCategory_edtSearch);
            _btnCancel = FindViewById<Button>(Resource.Id.FoodCategory_btnCancel);
            _imgDeleteText= FindViewById<ImageView>(Resource.Id.FoodCategory_imgDeleteText);
            _imgDeleteText.Visibility = ViewStates.Gone;
            _edtSearch.TextChanged += SearchData;
            _btnCancel.Click += delegate
            {
                Finish();
            };
            _imgDeleteText.Click += delegate
            {
                _edtSearch.Text = null;
            };
            setupData();
        }

        private void SearchData(object sender, TextChangedEventArgs e)
        {
            string content = _edtSearch.Text.Trim();
            if (!String.IsNullOrEmpty(content))
            {
                _imgDeleteText.Visibility = ViewStates.Visible;
                _listMonAn_Filtered = _listMonAn.Where(x => x.TenMon.Contains(content)).ToList();
            }
            else
            {
                _imgDeleteText.Visibility = ViewStates.Gone;
                _listMonAn_Filtered = _listMonAn;
            }
            HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn_Filtered, _category);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.FoodCategory_RecyclerView_Data);
            _recyclerData.SetAdapter(adapter);
            _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
        }


        private void setupData()
        {
            _listMonAn = SQLiteDataHandler.BeanMonAn_LoadList();
            filterData();
            HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn_Filtered, _category);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.FoodCategory_RecyclerView_Data);
            _recyclerData.SetAdapter(adapter);
            _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
        }
        private void filterData()
        {
            if (_category == 0)
            {
                _listMonAn_Filtered = _listMonAn;
            }
            else if (_category == 1)
            {
                _listMonAn_Filtered = _listMonAn.Where(x => x.MaLoai == 1).ToList();
            }
            else if (_category == 2)
            {
                _listMonAn_Filtered = _listMonAn.Where(x => x.MaLoai == 2).ToList();
            }
            else if (_category == 3)
            {
                _listMonAn_Filtered = _listMonAn.Where(x => x.MaLoai == 3).ToList();
            }
        }
    }
}