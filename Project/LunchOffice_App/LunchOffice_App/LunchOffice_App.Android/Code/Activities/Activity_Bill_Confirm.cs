using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Cmm;
using LunchOffice_App.Droid.Code.SQLite;
using static Android.App.ActionBar;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Bill_Confirm", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Bill_Confirm : Activity
    {
        private Button _btnClose, _btnChangeAddress, _btnChangePhone, _btnChangeNote, _btnOrder;
        private TextView _tvAddress, _tvPhone, _tvNote, _tvPrice;
        private RecyclerView _recyclerData;
        private AlertDialog _dialog;
        private Spinner _spinnerDistrict, _spinnerWard;
        string _tempPhone, _tempAddress, _tempNote;
        private List<BeanSession> list = new List<BeanSession>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            SetData();
        }

        private void SetData()
        {
            list = SQLiteDataHandler.BeanSession_LoadList();
        }

        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Bill_Confirm);
            _btnClose = FindViewById<Button>(Resource.Id.BillConfirm_btnClose);
            _btnChangeAddress = FindViewById<Button>(Resource.Id.BillConfirm_btnChangeAddress);
            _btnChangePhone = FindViewById<Button>(Resource.Id.BillConfirm_btnChangePhone);
            _btnChangeNote = FindViewById<Button>(Resource.Id.BillConfirm_btnChangeNote);
            _btnOrder = FindViewById<Button>(Resource.Id.BillConfirm_btnConfirm);
            _tvAddress = FindViewById<TextView>(Resource.Id.BillConfirm_tvAddress);
            _tvPhone = FindViewById<TextView>(Resource.Id.BillConfirm_tvPhone);
            _tvNote = FindViewById<TextView>(Resource.Id.BillConfirm_tvNote);
            _tvPrice = FindViewById<TextView>(Resource.Id.BillConfirm_tvPrice);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.BillConfirm_recyclerData);

            _btnClose.Click += delegate{ Finish(); };

            _btnChangeAddress.Click += Click_ChangeAddress;

            _btnChangePhone.Click += Click_ChangePhone;

            _btnChangeNote.Click += Click_ChangeNote;

            if (CmmVar.LIST_SHOPPING_CART.Count >0)
            {
                List<BeanShoppingCart> newl = CmmVar.LIST_SHOPPING_CART;
            }
        }

        private void Click_ChangeAddress(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            View root = LayoutInflater.Inflate(Resource.Layout.Layout_Bill_Confirm_Change_Address, null);
            EditText _edtAddresss = root.FindViewById<EditText>(Resource.Id.BillConfirm_Address_edtAddress);
            _spinnerWard = root.FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerWard);
            _spinnerDistrict = root.FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerDistrict);
            Button _btnAddresss = root.FindViewById<Button>(Resource.Id.BillConfirm_Note_btnConfirm);


            //Spinner
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_DISTRICT);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            _spinnerDistrict.Adapter = adapter;
            _spinnerDistrict.ItemSelected += spinnerDistrictItemSelected;

            _btnAddresss.Click += delegate
            {
                if (!String.IsNullOrEmpty(_edtAddresss.Text))
                {
                    _tempAddress = _edtAddresss.Text + " " + _spinnerWard.SelectedItem.ToString() + " " + _spinnerDistrict.SelectedItem.ToString();
                    _tvAddress.Text = _tempAddress;
                    Toast.MakeText(this, "Cập nhật thành công!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Thông tin không được rỗng!", ToastLength.Long).Show();
                }
            };
            DisplayMetrics displayMetrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int height = displayMetrics.HeightPixels;
            int width = displayMetrics.WidthPixels;

            builder.SetView(root);
            _dialog = builder.Create();
            _dialog.Window.SetLayout(LayoutParams.MatchParent, LayoutParams.MatchParent);
            _dialog.Show();
        }
        private void Click_ChangePhone(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            View root = LayoutInflater.Inflate(Resource.Layout.Layout_Bill_Confirm_Change_Phone, null);
            EditText _edtPhonee = root.FindViewById<EditText>(Resource.Id.BillConfirm_Phone_edtPhone);
            Button _btnPhonee = root.FindViewById<Button>(Resource.Id.BillConfirm_Phone_btnConfirm);

            _btnPhonee.Click += delegate
            {
                if (!String.IsNullOrEmpty(_edtPhonee.Text))
                {
                    if (_edtPhonee.Text.All(char.IsDigit)) // string la chuoi so
                    {
                        _tempPhone = _edtPhonee.Text;
                        _tvPhone.Text = _tempPhone;
                        Toast.MakeText(this, "Cập nhật thành công!", ToastLength.Long).Show();
                    }
                    else // co chu trong string
                    {
                        Toast.MakeText(this, "Nội dung nhập vào phải là chuỗi số!", ToastLength.Long).Show();
                    }
                }
                else
                {
                    Toast.MakeText(this, "Thông tin không được rỗng!", ToastLength.Long).Show();
                }
            };

            builder.SetView(root);
            _dialog = builder.Create();
            _dialog.Show();
        }
        private void Click_ChangeNote(object sender, EventArgs e)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            View root = LayoutInflater.Inflate(Resource.Layout.Layout_Bill_Confirm_Change_Note, null);
            EditText _edtNotee = root.FindViewById<EditText>(Resource.Id.BillConfirm_Note_edtNote);
            Button _btnNotee = root.FindViewById<Button>(Resource.Id.BillConfirm_Note_btnConfirm);

            _btnNotee.Click += delegate
            {
                if (!String.IsNullOrEmpty(_edtNotee.Text))
                {
                    _tempNote = _edtNotee.Text;
                    _tvNote.Text = _tempNote;
                    Toast.MakeText(this, "Cập nhật thành công!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Thông tin không được rỗng!", ToastLength.Long).Show();
                }
            };

            builder.SetView(root);
            _dialog = builder.Create();
            _dialog.Show();
        }
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

    }
}