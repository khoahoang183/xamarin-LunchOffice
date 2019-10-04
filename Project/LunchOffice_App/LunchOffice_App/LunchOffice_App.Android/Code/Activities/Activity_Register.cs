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

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Register", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Register : Activity
    {
        EditText _edtID, _edtPassword, _edtConfirmPassword, _edtAddress, _edtPhone, _edtEmail;
        Button btnRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            loadTestData();
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Login_Register);
            _edtID = FindViewById<EditText>(Resource.Id.Register_edtTaiKhoan);
            _edtPassword = FindViewById<EditText>(Resource.Id.Register_edtMatKhau);
            _edtConfirmPassword = FindViewById<EditText>(Resource.Id.Register_edtConfirmMatKhau);
            _edtAddress = FindViewById<EditText>(Resource.Id.Register_edtDiaChi);
            _edtPhone = FindViewById<EditText>(Resource.Id.Register_edtSoDienThoai);
            _edtEmail = FindViewById<EditText>(Resource.Id.Register_edtEmail);
            btnRegister = FindViewById<Button>(Resource.Id.Register_btnRegister);

            btnRegister.Click += btnRegisterClick;
        }

        private void btnRegisterClick(object sender, EventArgs e)
        {
            string _checkAPI = "OK";
            if (checkValidateData() == true)
            {
                // send Jsonlen check server ...
                if (_checkAPI.Equals("OK"))
                {
                    Toast.MakeText(this, "Đăng ký thành công!", ToastLength.Long).Show();
                    System.Threading.Thread.Sleep(3000);
                    Finish();
                }
                else
                {
                    Toast.MakeText(this, "Đăng ký thất bại!", ToastLength.Long).Show();
                }

            }              
        }

        private bool checkValidateData()
        {
            bool _result = true;
            string _resultText = "";
            if (_edtID.Text.Equals("") || _edtPassword.Text.Equals("") || _edtConfirmPassword.Text.Equals("") ||
                _edtAddress.Text.Equals("") || _edtPhone.Text.Equals("") || _edtEmail.Text.Equals(""))
            {
                if (_edtID.Text.Equals("")) _edtID.RequestFocus();
                if (_edtPassword.Text.Equals("")) _edtPassword.RequestFocus();
                if (_edtConfirmPassword.Text.Equals("")) _edtConfirmPassword.RequestFocus();
                if (_edtAddress.Text.Equals("")) _edtAddress.RequestFocus();
                if (_edtPhone.Text.Equals("")) _edtPhone.RequestFocus();
                if (_edtEmail.Text.Equals("")) _edtEmail.RequestFocus();
                _result = false;
                _resultText += "Vui lòng nhập đủ dữ liệu!";
            }
            else // du lieu khac null
            {
                if (!_edtPassword.Text.Equals(_edtConfirmPassword.Text))
                {
                    _result = false;
                    _resultText += "Mật khẩu nhập lại không đúng!";
                }
                else if (!_edtEmail.Text.Contains("@"))
                {
                    _result = false;
                    _resultText += "Email phải có dạng abc@xyz!";
                }
            }
            if (!_resultText.Equals("")) Toast.MakeText(this, _resultText, ToastLength.Long).Show();
            return _result;           
        }

        private void loadTestData()
        {
            _edtID.Text = "khoatest";
            _edtPassword.Text = "password";
            _edtConfirmPassword.Text = "password";
            _edtAddress.Text = "test";
            _edtPhone.Text = "123456789";
            _edtEmail.Text = "khoa@gmail.com";
        }
    }
}