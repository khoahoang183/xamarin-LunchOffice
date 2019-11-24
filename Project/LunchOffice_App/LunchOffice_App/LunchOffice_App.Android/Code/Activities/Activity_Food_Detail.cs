using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Cmm;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Food_Detail", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Food_Detail : Activity
    {
        private ImageView _imgPortrait;
        private TextView _tvFoodName, _tvFoodPrice, _tvFoodDescription;
        private Button _btnOrder;
        AlertDialog _dialog;
        BeanMonAn _beanMonAn = new BeanMonAn();
        int MaMon;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            getLayout();
            setupData();
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Food_Detail);
            _imgPortrait = FindViewById<ImageView>(Resource.Id.FoodDetail_imgPortrait);
            _tvFoodName = FindViewById<TextView>(Resource.Id.FoodDetail_tvFoodName);
            _tvFoodPrice = FindViewById<TextView>(Resource.Id.FoodDetail_tvFoodPrice);
            _tvFoodDescription = FindViewById<TextView>(Resource.Id.FoodDetail_tvFoodDescription);
            _btnOrder = FindViewById<Button>(Resource.Id.FoodDetail_btnOrder);
            _btnOrder.Click += Click_DialogOrder;
        }
        private void Click_DialogOrder(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            View root = LayoutInflater.Inflate(Resource.Layout.Layout_Food_Detail_AddItem, null);
            TextView _tvFoodName_Detail = root.FindViewById<TextView>(Resource.Id.AddItem_tvFoodName);
            TextView _tvPrice_Detail = root.FindViewById<TextView>(Resource.Id.AddItem_tvPrice);
            TextView _tvCount_Detail = root.FindViewById<TextView>(Resource.Id.AddItem_tvCount);
            Button _btnDecrease_Detail = root.FindViewById<Button>(Resource.Id.AddItem_btnDecrease);
            Button _btnIncrease_Detail = root.FindViewById<Button>(Resource.Id.AddItem_btnIncrease);
            Button _btnAdd_Detail = root.FindViewById<Button>(Resource.Id.AddItem_btnAdd);

            _tvFoodName_Detail.Text = _beanMonAn.TenMon.ToString();
            _tvPrice_Detail.Text = String.Format("{0:#,0}", _beanMonAn.GiaTien).ToString() + "đ";
            _btnIncrease_Detail.Click += delegate
            {
                int _temp = int.Parse(_tvCount_Detail.Text);
                _temp = _temp + 1;
                _tvCount_Detail.Text = _temp.ToString();               
                _tvPrice_Detail.Text = String.Format("{0:#,0}", _beanMonAn.GiaTien * (_temp * 1.0)).ToString() + "đ";
            };
            _btnDecrease_Detail.Click += delegate
            {
                int _temp = int.Parse(_tvCount_Detail.Text);
                if (_temp != 1)
                {
                    _temp = _temp - 1;
                    _tvCount_Detail.Text = _temp.ToString();
                    _tvPrice_Detail.Text = String.Format("{0:#,0}", _beanMonAn.GiaTien * (_temp * 1.0)).ToString() + "đ";
                }
            };
            _btnAdd_Detail.Click += delegate
            {
                _dialog.Dismiss();
                BeanShoppingCart temp = new BeanShoppingCart(MaMon, int.Parse(_tvCount_Detail.Text));
                CmmVar.LIST_SHOPPING_CART.Add(temp);
                Toast.MakeText(this, "Đã thêm vào giỏ hàng", ToastLength.Long).Show();
            };
            builder.SetView(root);
            _dialog = builder.Create();
            _dialog.Show();
        }

        #region Data
        private void setupData()
        {
            if (Intent != null)
            {
                MaMon = int.Parse(Intent.GetStringExtra("Detail_MaMon"));
                _beanMonAn = SQLite.SQLiteDataHandler.BeanMonAn_SearchItem(MaMon);
                if (_beanMonAn != null)
                {
                    Utilities.Utilities_LoadImage.LoadImageToImageView(_beanMonAn.HinhAnh, _imgPortrait);
                    _tvFoodName.Text = _beanMonAn.TenMon.ToString();
                    _tvFoodPrice.Text = String.Format("{0:#,0}", _beanMonAn.GiaTien) + "đ";
                    _tvFoodDescription.Text = _beanMonAn.MieuTa.ToString();
                }
            }
        }
        #endregion
    }
}