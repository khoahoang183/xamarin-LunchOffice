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
using LunchOffice_App.Droid.Code.Utilities;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Register", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Register : Activity
    {
        EditText _edtID, _edtPassword, _edtConfirmPassword, _edtAddress, _edtPhone, _edtEmail;
        Spinner _spinnerDistrict, _spinnerWard;
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

            _spinnerDistrict = FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerDistrict);
            //_spinnerWard = FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerWard);

            btnRegister.Click += btnRegisterClick;
            //setup data
            //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_DISTRICT);
            //adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //_spinnerDistrict.Adapter = adapter;
            //_spinnerDistrict.ItemSelected += spinnerDistrictItemSelected;
        }
        #region View Event
        private void spinnerDistrictItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ArrayAdapter<string> adapter;
            string _selectedDistrict = _spinnerDistrict.SelectedItem.ToString();
            if (_selectedDistrict.Equals("Quận 1"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN1);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 2"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN2);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 3"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN3);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 4"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN4);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 5"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN5);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 6"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN6);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 7"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN7);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 8"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN8);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 9"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN9);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 10"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN10);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 11"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN11);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
            if (_selectedDistrict.Equals("Quận 12"))
            {
                adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN12);
                adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
                _spinnerWard.Adapter = adapter;
            }
        }
        private async void btnRegisterClick(object sender, EventArgs e)
        {
            if (checkValidateData() == true)
            {
                await Utilities_API.API_GetRegister(_edtID.Text, _edtEmail.Text, _edtPassword.Text);
                BeanNguoiDung result = Utilities_API.RESULT_APIREGISTER_BEANNGUOIDUNG;
                if (result != null) // dang ky thanh cong
                {
                    if (!String.IsNullOrEmpty(result.MaNguoiDung) && !String.IsNullOrEmpty(result.TaiKhoan) && !String.IsNullOrEmpty(result.MatKhau)
                        && result.KichHoat == false) // thanh cong - chua kich hoat
                    {
                        Toast.MakeText(this, "Đăng ký thành công!", ToastLength.Long).Show();
                        Finish();
                        Intent intent = new Intent(this, typeof(Activity_Login_ConfirmOTP));
                        intent.PutExtra("Email", result.Email);
                        intent.PutExtra("MaNguoiDung", result.MaNguoiDung);
                        intent.PutExtra("TaiKhoan", result.TaiKhoan);
                        intent.PutExtra("MatKhau", result.MatKhau);
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast.MakeText(this, "Đăng ký thất bại!", ToastLength.Long).Show();
                    }
                }
            }
        }
        #endregion
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
            //_edtID.Text = "khoauser";
            //_edtPassword.Text = "Aa123456";
            //_edtConfirmPassword.Text = "Aa123456";
            _edtAddress.Text = "123 Nguyễn Trãi";
            _edtPhone.Text = "0834673896";
            _edtEmail.Text = "hoangdangkhoa.m9@gmail.com";
        }
    }
}