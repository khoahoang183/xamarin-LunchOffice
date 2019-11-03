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
using LunchOffice_App.Droid.Code.SQLite;
using LunchOffice_App.Droid.Code.Utilities;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Login_ConfirmOTP", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Login_ConfirmOTP : Activity
    {
        EditText _edtOTP;
        Button _btn0, _btn1, _btn2, _btn3, _btn4, _btn5, _btn6, _btn7, _btn8, _btn9, _btnSend, _btnDel;
        private string _MaNguoiDung, _TaiKhoan, _MatKhau;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Intent != null)
            {
                _MaNguoiDung = Intent.GetStringExtra("MaNguoiDung");
                _TaiKhoan = Intent.GetStringExtra("TaiKhoan");
                _MatKhau = Intent.GetStringExtra("MatKhau");
            }
            getLayout();
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Login_Register_ConfirmOTP);
            _edtOTP = FindViewById<EditText>(Resource.Id.ConfirmOTP_edtOTP);
            _btn1 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn1);
            _btn2 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn2);
            _btn3 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn3);
            _btn4 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn4);
            _btn5 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn5);
            _btn6 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn6);
            _btn7 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn7);
            _btn8 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn8);
            _btn9 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn9);
            _btn0 = FindViewById<Button>(Resource.Id.ConfirmOTP_btn0);
            _btnSend = FindViewById<Button>(Resource.Id.ConfirmOTP_btnSend);
            _btnDel = FindViewById<Button>(Resource.Id.ConfirmOTP_btnDelete);

            _edtOTP.Enabled = false;
            _btn0.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "0";
                    _edtOTP.Text = temp;
                }
            };
            _btn1.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "1";
                    _edtOTP.Text = temp;
                }
            };
            _btn2.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "2";
                    _edtOTP.Text = temp;
                }
            };
            _btn3.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "3";
                    _edtOTP.Text = temp;
                }
            };
            _btn4.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "4";
                    _edtOTP.Text = temp;
                }
            };
            _btn5.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "5";
                    _edtOTP.Text = temp;
                }
            };
            _btn6.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "6";
                    _edtOTP.Text = temp;
                }
            };
            _btn7.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "7";
                    _edtOTP.Text = temp;
                }
            };
            _btn8.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "8";
                    _edtOTP.Text = temp;
                }
            };
            _btn9.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length < 6)
                {
                    temp = temp + "9";
                    _edtOTP.Text = temp;
                }
            };
            _btnDel.Click += delegate
            {
                string temp = _edtOTP.Text;
                if (temp.Length > 0)
                {
                    temp = temp.Remove(temp.Length - 1, 1);
                    _edtOTP.Text = temp;
                }
            };
            _btnSend.Click += SendOTP;
        }

        private async void SendOTP(object sender, EventArgs e)
        {
            if (_edtOTP.Text.Length == 6)
            {
                if (!String.IsNullOrEmpty(_MaNguoiDung))
                {
                    // get List Mon An
                    await Utilities_API.API_ConfirmOTP(_MaNguoiDung, _edtOTP.Text);
                    bool result = Utilities_API.RESULT_APIOTP_BOOL;
                    if (result == true)
                    {
                        Toast.MakeText(this, "Xác nhận OTP thành công!", ToastLength.Long).Show();
                        // Add session
                        BeanSession session = new BeanSession(_MaNguoiDung, _TaiKhoan, _MatKhau);
                        SQLiteDataHandler.BeanSession_AddSession(session);
                        Finish();
                        Intent intent = new Intent(this, typeof(Activity_Home));
                        StartActivity(intent);
                    }
                    else if (result == false)
                    {
                        Toast.MakeText(this, "Xác nhận OTP thất bại!", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Tài khoản có lỗi xảy ra", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Mã OTP không hợp lệ", ToastLength.Long).Show();
            }

        }
    }
}