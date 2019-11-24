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
using LunchOffice_App.Droid.Code.Cmm;
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
            setupData();
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

        }

        private void SearchData(object sender, TextChangedEventArgs e)
        {

            _listMonAn_Filtered = new List<BeanMonAn>();
            if (!String.IsNullOrEmpty(_edtSearch.Text))
            {
                string content = CmmFunction.RemoveVietNamAccent(_edtSearch.Text.Trim().ToLowerInvariant());
                _imgDeleteText.Visibility = ViewStates.Visible;
                foreach (BeanMonAn item in _listMonAn)
                {
                    string _name = CmmFunction.RemoveVietNamAccent(item.TenMon).ToLowerInvariant();
                    string _search = CmmFunction.RemoveVietNamAccent(item.SearchData).ToLowerInvariant();
                    if (_name.Contains(content) || _search.Contains(content))
                    {
                        _listMonAn_Filtered.Add(item);
                    }
                }
            }
            else
            {
                _imgDeleteText.Visibility = ViewStates.Gone;
                _listMonAn_Filtered = _listMonAn;
            }
            HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn_Filtered, _category);
            adapter.ItemClick += Click_RecyclerData;
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.FoodCategory_RecyclerView_Data);
            _recyclerData.SetAdapter(adapter);
            _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
        }
        private void setupData()
        {
            _listMonAn = SQLiteDataHandler.BeanMonAn_LoadList();
            filterData();
            HomeRecyclerViewAdapter adapter = new HomeRecyclerViewAdapter(this, _listMonAn_Filtered, _category);
            adapter.ItemClick += Click_RecyclerData;
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.FoodCategory_RecyclerView_Data);
            _recyclerData.SetAdapter(adapter);
            _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
        }
        private void filterData()
        {
            if (_category == 0)
            {
                _listMonAn = _listMonAn;
            }
            else if (_category == 1)
            {
                _listMonAn = _listMonAn.Where(x => x.MaLoai == 1).ToList();
            }
            else if (_category == 2)
            {
                _listMonAn = _listMonAn.Where(x => x.MaLoai == 2).ToList();
            }
            else if (_category == 3)
            {
                _listMonAn = _listMonAn.Where(x => x.MaLoai == 3).ToList();
            }
            _listMonAn_Filtered = _listMonAn;
        }
        private void Click_RecyclerData(object sender, int e)
        {
            Intent intent = new Intent(this, typeof(Activity_Food_Detail));
            intent.PutExtra("Detail_MaMon", _listMonAn_Filtered[e].MaMon.ToString());
            StartActivity(intent);
        }
    }
}