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
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Adapter;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Utilities;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Bill_Confirm_Address", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Bill_Confirm_Address : Activity
    {
        Button _btnClose, _btnChangeAddress;
        List<BeanDiaChi> _lstDiaChi = new List<BeanDiaChi>();
        Spinner _spinnerDistrict, _spinnerWard;
        private RecyclerView _recyclerData;
        private int _IsChoosenIndex = -1;
        private string MaNguoiDung = "";
        public static BeanDiaChi _DiaChiSelected = new BeanDiaChi();
        public static float SHIPPINGFEE = -1;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
             _DiaChiSelected = new BeanDiaChi();
            if (Intent != null)
            {
                MaNguoiDung = Intent.GetStringExtra("MaNguoiDung");
                if (!String.IsNullOrEmpty(MaNguoiDung))
                {
                    await Utilities_API.API_GetListDiaChiByMaNguoiDung(MaNguoiDung);
                    _lstDiaChi = Utilities_API.RESULT_APIGET_LISTDIACHI_BYMANGUOIDUNG;                   
                }
            }
            getLayout();
            SetData();
        }
        private void getLayout()
        {
            SHIPPINGFEE = -1;
            SetContentView(Resource.Layout.Layout_Bill_Confirm_Change_Address);
            _btnClose = FindViewById<Button>(Resource.Id.BillConfirm_Address_btnClose);
            _btnChangeAddress = FindViewById<Button>(Resource.Id.BillConfirm_Address_btnConfirm);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.BillConfirm_Address_recyData);
            _spinnerDistrict = FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerDistrict);
            _spinnerWard = FindViewById<Spinner>(Resource.Id.BillConfirm_Address_spinnerWard);

            _btnClose.Click += delegate
            {
                Finish();
            };
            _btnChangeAddress.Click += async delegate
            {
             
            };
        }
        private void Click_Recy_Delete(object sender, int e)
        {

        }
        private void Click_Recy_Edit(object sender, int e)
        {

        }
        private void Click_Recy_Choose(object sender, int e)
        {
            _DiaChiSelected = _lstDiaChi[e];
            Finish();
        }
        private void SetData()
        {
            if (_lstDiaChi != null && _lstDiaChi.Count > 0)
            {
                _lstDiaChi = _lstDiaChi.OrderByDescending(x => x.MacDinh).ToList();
                _IsChoosenIndex = getMacDinhIndex(_lstDiaChi);
                BillConfirmAddressRecyclerViewAdapter adapter = new BillConfirmAddressRecyclerViewAdapter(_lstDiaChi, _IsChoosenIndex);
                adapter.Click_Choose += Click_Recy_Choose;
                _recyclerData.SetAdapter(adapter);
                _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
            }
            ArrayAdapter<string> adapterr = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_DISTRICT);
            adapterr.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            _spinnerDistrict.Adapter = adapterr;
            _spinnerDistrict.ItemSelected += spinnerDistrictItemSelected;
        }
        private int getMacDinhIndex(List<BeanDiaChi> lstdiachi)
        {
            return _lstDiaChi.FindIndex(x => x.MacDinh == true);
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