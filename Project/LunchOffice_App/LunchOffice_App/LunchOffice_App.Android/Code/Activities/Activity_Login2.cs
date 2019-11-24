using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Activity(Label = "Activity_Login2", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Login2 : Activity
    {
        TextView _tvRegister;
        Button _btnLogin, _btnGoogle, _btnFacebook;
        EditText _edtEmail, _edtPassword;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Login_2);
            _tvRegister = FindViewById<TextView>(Resource.Id.tvRegister);
            _btnLogin = FindViewById<Button>(Resource.Id.Login2_btnLogin);
            _btnGoogle = FindViewById<Button>(Resource.Id.Login2_btnGoogle);
            _btnFacebook = FindViewById<Button>(Resource.Id.Login2_btnFacebook);
            _edtEmail = FindViewById<EditText>(Resource.Id.Login2_edtEmail);
            _edtPassword = FindViewById<EditText>(Resource.Id.Login2_edtPassword);
            _btnGoogle.Enabled = _btnFacebook.Enabled = false;
            _tvRegister.Click += tvRegisterClick;
            _btnLogin.Click += Click_Login;
        }

        private async void Click_Login(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_edtEmail.Text) || String.IsNullOrEmpty(_edtPassword.Text))
            {
                Toast.MakeText(this, "Vui lòng nhập đủ thông tin!", ToastLength.Long).Show();
            }
            else // nhap du thong tin
            {
                //Intent intent = new Intent(this, typeof(Activity_Login_ConfirmOTP));
                //StartActivity(intent);

                await Utilities_API.API_GetLogIn(_edtEmail.Text, _edtPassword.Text);
                BeanNguoiDung result = Utilities_API.RESULT_APILOGIN_BEANNGUOIDUNG;
                if (result != null)
                {
                    if (!String.IsNullOrEmpty(result.MaNguoiDung) && !String.IsNullOrEmpty(result.TaiKhoan) &&
                        !String.IsNullOrEmpty(result.MatKhau)) // thanh cong
                    {
                        // kiem tra coi co confirm otp chua
                        if (result.KichHoat == true) // da kich hoat
                        {
                            BeanSession session = new BeanSession(result.MaNguoiDung, result.TaiKhoan, result.MatKhau, result.HoTen, result.GioiTinh,
                                result.NgaySinh, result.HinhAnh, result.Email, result.LoaiNguoiDung, result.KhoaNguoiDung, result.MaOtp, result.KichHoat,
                                result.Created, result.Modified, result.CreatedBy, result.ModifiedBy);
                            SQLiteDataHandler.BeanSession_AddSession(session);
                            Finish();
                            Intent intent = new Intent(this, typeof(Activity_Home));
                            StartActivity(intent);
                        }
                        else // chua kich hoat
                        {
                            Finish();
                            Intent intent = new Intent(this, typeof(Activity_Login_ConfirmOTP));
                            intent.PutExtra("MaNguoiDung", result.MaNguoiDung);
                            intent.PutExtra("TaiKhoan", result.TaiKhoan);
                            intent.PutExtra("MatKhau", result.MatKhau);
                            StartActivity(intent);
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Đăng nhập thất bại!", ToastLength.Long).Show();
                    }
                }
            }
        }

        private void tvRegisterClick(object sender, EventArgs e)
        {
            Finish();
            Intent intent = new Intent(this, typeof(Activity_Register));
            StartActivity(intent);
        }

    }
}