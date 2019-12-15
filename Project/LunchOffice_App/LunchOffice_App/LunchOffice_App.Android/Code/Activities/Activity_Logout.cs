using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Cmm;
using LunchOffice_App.Droid.Code.SQLite;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Logout", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Logout : Activity
    {
        List<BeanSession> list = new List<BeanSession>();
        Button _btnLogout;
        private ImageView _imgShowPassword;
        private Button _btnClose;
        private EditText _edtID, _edtPassword, _edtEmail, _edtGioiTinh, _edtNgaySinh;
        private int _flagPassword = 0; // 1 = show
        private string _passHide = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            setData();
        }
        private void setData()
        {
            // check session
            list = SQLiteDataHandler.BeanSession_LoadList();
            if (list != null && list.Count > 0) // Co session
            {

                if (!String.IsNullOrEmpty(list[0].TaiKhoan))
                {
                    _edtID.Text = list[0].TaiKhoan;
                }
                if (!String.IsNullOrEmpty(list[0].Email))
                {
                    _edtEmail.Text = list[0].Email;
                }
                if (!String.IsNullOrEmpty(list[0].MatKhau))
                {
                    for (int i = 0; i < list[0].MatKhau.Length; i++)
                    {
                        _passHide += "*";
                    }
                    _edtPassword.Text = _passHide;
                }
                if (list[0].NgaySinh != null)
                {
                    _edtNgaySinh.Text = list[0].NgaySinh.Value.Date.ToString("d");
                }
                if (list[0].GioiTinh == true)
                {
                    _edtGioiTinh.Text = "Nam";
                }
                else
                {
                    _edtGioiTinh.Text = "Nữ";
                }
            }

        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Logout);
            _btnLogout = FindViewById<Button>(Resource.Id.Logout_btnLogout);
            _edtPassword = FindViewById<EditText>(Resource.Id.edt_Logout_Password);
            _edtEmail = FindViewById<EditText>(Resource.Id.edt_Logout_Email);
            _edtID = FindViewById<EditText>(Resource.Id.edt_Logout_TenTaiKhoan);
            _edtGioiTinh = FindViewById<EditText>(Resource.Id.edt_Logout_GioiTinh);
            _edtNgaySinh = FindViewById<EditText>(Resource.Id.edt_Logout_NgaySinh);
            _btnClose = FindViewById<Button>(Resource.Id.img_Logout_Close);           
            _imgShowPassword = FindViewById<ImageView>(Resource.Id.img_Logout_ShowPassword);
            _edtPassword.Enabled = _edtID.Enabled = _edtEmail.Enabled = _edtGioiTinh.Enabled = _edtNgaySinh.Enabled = false;
            _btnClose.Click += Click_Close;
            _btnLogout.Click += Click_Logout;
            _imgShowPassword.Click += Click_ShowPassword;
        }

        private void Click_ShowPassword(object sender, EventArgs e)
        {
            if (_flagPassword == 0)
            {
                _flagPassword = 1;
                _edtPassword.Text = list[0].MatKhau;
            }
            else if (_flagPassword == 1)
            {
                _flagPassword = 0;
                _edtPassword.Text = _passHide;
            }
        }
        private void Click_Close(object sender, EventArgs e)
        {
            Finish();
        }
        private void Click_Logout(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Thông báo");
            alert.SetMessage("Bạn có muốn đăng xuất không?");
            alert.SetButton2("Có", (c, ev) =>
            {
                dialog.Dispose();
                list = SQLiteDataHandler.BeanSession_LoadList();
                if (list != null && list.Count > 0)
                {
                    // xoa gio hang 
                    CmmVar.LIST_SHOPPING_CART = new List<BeanShoppingCart>();
                    SQLiteDataHandler.BeanSession_ClearSession();
                    Intent intent = new Intent(this, typeof(Activity_Home));
                    StartActivity(intent);
                }
            });
            alert.SetButton("Không", (c, ev) =>
            {
                dialog.Dispose();
            });
            alert.Show();
        }
    }
}