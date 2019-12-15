using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Adapter;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Cmm;
using LunchOffice_App.Droid.Code.SQLite;
using LunchOffice_App.Droid.Code.Utilities;
using static Android.App.ActionBar;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Bill_Confirm", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Bill_Confirm : Activity
    {
        private View _view;
        private Button _btnClose, _btnChangeAddress, _btnChangePhone, _btnChangeNote, _btnOrder;
        private TextView _tvAddress, _tvPhone, _tvNote, _tvPrice, _tvShip;
        private RecyclerView _recyclerData;
        private AlertDialog _dialog;
        private Spinner _spinnerDistrict, _spinnerWard;
        string _tempPhone, _tempAddress, _tempNote;
        private List<BeanSession> list = new List<BeanSession>();
        private List<BeanDiaChi> _lstDiaChi = new List<BeanDiaChi>();
        private List<BeanShoppingCart> _lstCart = new List<BeanShoppingCart>();
        private BeanDiaChi _currentBeanDiaChi = new BeanDiaChi();
        private string _MaNguoiDung = "";
        private BeanDiaChi _selectedBeanDiaChi = new BeanDiaChi();
        private float _currentmoney = 0;
        private float _currentship = 0;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            list = SQLiteDataHandler.BeanSession_LoadList();
            _lstCart = SQLiteDataHandler.BeanShoppingCart_LoadList();
            if (_btnClose == null)
            {
                if (list != null && list.Count > 0) // Co session
                {
                    _MaNguoiDung = list[0].MaNguoiDung;
                    if (!String.IsNullOrEmpty(_MaNguoiDung)) // keo API dia chi
                    {
                        await Utilities_API.API_GetListDiaChiByMaNguoiDung(_MaNguoiDung);
                        _lstDiaChi = Utilities_API.RESULT_APIGET_LISTDIACHI_BYMANGUOIDUNG;
                        if (_lstDiaChi != null && _lstDiaChi.Count > 0)
                        {
                            List<BeanDiaChi> temp = _lstDiaChi.Where(x => x.MacDinh == true).ToList();
                            await Utilities_API.API_GetShipFee(generateDistrict(temp[0].QuanHuyen));
                            _selectedBeanDiaChi = temp[0];
                            _currentship = Utilities_API.RESULT_API_COUNTSHIP;
                        }
                    }
                }
                else // chua co session
                {

                }
            }
            getLayout();
            SetData();
        }
        protected override async void OnResume()
        {
            base.OnResume();
            _selectedBeanDiaChi = new BeanDiaChi();
            _currentship = -1;
            if (Activity_Bill_Confirm_Address._DiaChiSelected != null && !String.IsNullOrEmpty(Activity_Bill_Confirm_Address._DiaChiSelected.SoNha))
            {
                _selectedBeanDiaChi = Activity_Bill_Confirm_Address._DiaChiSelected;
                await Utilities_API.API_GetShipFee(generateDistrict(_selectedBeanDiaChi.QuanHuyen));
                _currentship = Utilities_API.RESULT_API_COUNTSHIP;
                if (_tvAddress != null)
                {
                    _tvAddress.Text = _selectedBeanDiaChi.SoNha + " " + _selectedBeanDiaChi.PhuongXa + " " + _selectedBeanDiaChi.QuanHuyen + " " + _selectedBeanDiaChi.TinhThanh;

                }
                if (_tvShip != null)
                {
                    if (_currentship == -1)
                    {
                        _tvShip.Text = "Không giao hàng";
                        _tvShip.SetTextColor(Color.Red);
                    }
                    else
                    {
                        _tvShip.Text = String.Format("{0:#,0}", _currentship) + " VNĐ";
                        _tvShip.SetTextColor(Color.Black);
                    }
                }
            }
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
            _tvShip = FindViewById<TextView>(Resource.Id.BillConfirm_tvShip);
            _tvPhone = FindViewById<TextView>(Resource.Id.BillConfirm_tvPhone);
            _tvNote = FindViewById<TextView>(Resource.Id.BillConfirm_tvNote);
            _tvPrice = FindViewById<TextView>(Resource.Id.BillConfirm_tvPrice);
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.BillConfirm_recyclerData);

            _btnClose.Click += delegate { Finish(); };

            _btnChangeAddress.Click += Click_ChangeAddress;

            _btnChangePhone.Click += Click_ChangePhone;

            _btnChangeNote.Click += Click_ChangeNote;

            _btnOrder.Click += Click_Order;

            if (CmmVar.LIST_SHOPPING_CART.Count > 0)
            {
                List<BeanShoppingCart> newl = CmmVar.LIST_SHOPPING_CART;
            }
        }

        private async void Click_Order(object sender, EventArgs e)
        {
            if (!_tvShip.Text.Equals("Không giao hàng") && _currentmoney > 0)
            {
                list = SQLiteDataHandler.BeanSession_LoadList();
                if (list != null && list.Count > 0) // Co session
                {
                    // don hang
                    BeanDonHang beanDonHang = new BeanDonHang();
                    beanDonHang.MaKH = list[0].MaNguoiDung;
                    if (!String.IsNullOrEmpty(_selectedBeanDiaChi.SoNha))
                    {
                        beanDonHang.DiaChi = _selectedBeanDiaChi.SoNha + " " + generateDistrict(_selectedBeanDiaChi.PhuongXa) + " " + generateDistrict(_selectedBeanDiaChi.QuanHuyen) + " " + _selectedBeanDiaChi.TinhThanh;
                    }
                    beanDonHang.SoDT = _selectedBeanDiaChi.SoDT;
                    beanDonHang.PhiVanChuyen = _currentship;
                    beanDonHang.ThanhTien = _currentmoney;
                    // CT don hang
                    List<BeanItemCart> _lstAddCart = new List<BeanItemCart>();
                    _lstCart = SQLiteDataHandler.BeanShoppingCart_LoadList();
                    foreach (BeanShoppingCart item in _lstCart)
                    {
                        BeanItemCart temp = new BeanItemCart();
                        temp.MaMon = item.MaMonAn;
                        temp.SoLuong = item.SoLuong;
                        _lstAddCart.Add(temp);
                    }
                    await Utilities_API.API_Order(beanDonHang, _lstAddCart);
                    bool res = Utilities_API.RESULT_APIADD_BILL;
                    if (res == true)
                    {
                        SQLiteDataHandler.BeanShoppingCart_ClearAll();

                        _lstCart = new List<BeanShoppingCart>();
                        ShoppingCartRecyclerViewAdapter adapter = new ShoppingCartRecyclerViewAdapter(this, _lstCart);
                        adapter.ClickDecrease += Click_ItemDecrease;
                        adapter.ClickIncrease += Click_ItemIncrease;
                        adapter.ItemClick += Click_DeleteItem;
                        _recyclerData.SetAdapter(adapter);
                        _recyclerData.SetLayoutManager(new LinearLayoutManager(this));

                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Thêm mới thành công");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            dialog.Dispose();
                            Finish();
                        });
                        alert.Show();
                    }
                    else
                    {
                        Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                        AlertDialog alert = dialog.Create();
                        alert.SetTitle("Thông báo");
                        alert.SetMessage("Thêm mới thất bại");
                        alert.SetButton("OK", (c, ev) =>
                        {
                            dialog.Dispose();
                        });
                        alert.Show();
                    }
                }
            }
            else
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Vui lòng nhập đủ thông tin");
                alert.SetButton("OK", (c, ev) =>
                {
                    dialog.Dispose();
                });
                alert.Show();
            }
        }
        private void Click_ChangeAddress(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(_MaNguoiDung))
            {
                Intent intent = new Intent(this, typeof(Activity_Bill_Confirm_Address));
                intent.PutExtra("MaNguoiDung", list[0].MaNguoiDung);
                StartActivity(intent);
            }
            else
            {

            }

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

        private void Click_DeleteItem(object sender, int e)
        {
            if (_lstCart[e] != null)
            {
                Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                AlertDialog alert = dialog.Create();
                alert.SetTitle("Xác nhận");
                alert.SetMessage("Bạn có muốn xoá không?");
                alert.SetButton("Có", (c, ev) =>
                {
                    bool res = SQLiteDataHandler.BeanShoppingCart_DeleteItem(_lstCart[e]);
                    if (res == true)
                    {
                        _lstCart = SQLiteDataHandler.BeanShoppingCart_LoadList();
                        ShoppingCartRecyclerViewAdapter adapter = new ShoppingCartRecyclerViewAdapter(this, _lstCart);
                        adapter.ClickDecrease += Click_ItemDecrease;
                        adapter.ClickIncrease += Click_ItemIncrease;
                        adapter.ItemClick += Click_DeleteItem;
                        _recyclerData.SetAdapter(adapter);
                        _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
                        _currentmoney = CountTotalMoney(_lstCart);
                        _tvPrice.Text = String.Format("{0:#,0}", _currentmoney) + " VNĐ";
                    }
                });
                alert.SetButton2("Không", (c, ev) =>
                {
                    alert.Dismiss();
                });
                alert.Show();
            }
        }
        private void Click_ItemIncrease(object sender, int e)
        {
            if (_lstCart[e] != null)
            {
                if (_lstCart[e].SoLuong < 99)
                {
                    _lstCart[e].SoLuong = _lstCart[e].SoLuong + 1;
                    SQLiteDataHandler.BeanShoppingCart_UpdateItem(_lstCart[e]);
                    _lstCart = SQLiteDataHandler.BeanShoppingCart_LoadList();
                    ShoppingCartRecyclerViewAdapter adapter = new ShoppingCartRecyclerViewAdapter(this, _lstCart);
                    adapter.ClickDecrease += Click_ItemDecrease;
                    adapter.ClickIncrease += Click_ItemIncrease;
                    adapter.ItemClick += Click_DeleteItem;
                    _recyclerData.SetAdapter(adapter);
                    _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
                    _currentmoney = CountTotalMoney(_lstCart);
                    _tvPrice.Text = String.Format("{0:#,0}", _currentmoney) + " VNĐ";
                }
            }
        }
        private void Click_ItemDecrease(object sender, int e)
        {
            if (_lstCart[e] != null)
            {
                if (_lstCart[e].SoLuong > 1)
                {
                    _lstCart[e].SoLuong = _lstCart[e].SoLuong - 1;
                    SQLiteDataHandler.BeanShoppingCart_UpdateItem(_lstCart[e]);
                    _lstCart = SQLiteDataHandler.BeanShoppingCart_LoadList();
                    ShoppingCartRecyclerViewAdapter adapter = new ShoppingCartRecyclerViewAdapter(this, _lstCart);
                    adapter.ClickDecrease += Click_ItemDecrease;
                    adapter.ClickIncrease += Click_ItemIncrease;
                    adapter.ItemClick += Click_DeleteItem;
                    _recyclerData.SetAdapter(adapter);
                    _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
                    _currentmoney = CountTotalMoney(_lstCart);
                    _tvPrice.Text = String.Format("{0:#,0}", _currentmoney) + " VNĐ";
                }
            }
        }
        private void spinnerDistrictItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            ArrayAdapter<string> adapter;
            string _selectedDistrict = _spinnerDistrict.SelectedItem.ToString();
            adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD);
            adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            _spinnerWard.Adapter = adapter;

            //if (_selectedDistrict.Equals("Quận 1"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 2"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN2);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 3"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN3);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 4"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN4);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 5"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN5);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 6"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN6);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 7"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN7);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 8"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN8);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 9"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN9);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 10"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN10);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 11"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN11);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
            //if (_selectedDistrict.Equals("Quận 12"))
            //{
            //    adapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, DataWardDistrict.LIST_WARD_QUAN12);
            //    adapter.SetDropDownViewResource(Resource.Layout.support_simple_spinner_dropdown_item);
            //    _spinnerWard.Adapter = adapter;
            //}
        }
        private void SetData()
        {
            if (!String.IsNullOrEmpty(_selectedBeanDiaChi.QuanHuyen))
            {
                _tvAddress.Text = _selectedBeanDiaChi.SoNha + " " + _selectedBeanDiaChi.PhuongXa + " " + _selectedBeanDiaChi.QuanHuyen + " " + _selectedBeanDiaChi.TinhThanh;
            }
            if (_lstCart != null && _lstCart.Count > 0)
            {
                ShoppingCartRecyclerViewAdapter adapter = new ShoppingCartRecyclerViewAdapter(this, _lstCart);
                adapter.ClickDecrease += Click_ItemDecrease;
                adapter.ClickIncrease += Click_ItemIncrease;
                adapter.ItemClick += Click_DeleteItem;
                _recyclerData.SetAdapter(adapter);
                _recyclerData.SetLayoutManager(new LinearLayoutManager(this));
            }
            if (_lstDiaChi != null && _lstDiaChi.Count > 0)
            {
                List<BeanDiaChi> temp = _lstDiaChi.Where(x => x.MacDinh == true).ToList();
                _currentBeanDiaChi = temp[0];
                _tvAddress.Text = "";
                if (!String.IsNullOrEmpty(_currentBeanDiaChi.PhuongXa) && !String.IsNullOrEmpty(_currentBeanDiaChi.SoNha) &&
                    !String.IsNullOrEmpty(_currentBeanDiaChi.QuanHuyen) && !String.IsNullOrEmpty(_currentBeanDiaChi.TinhThanh))
                {

                    _tvAddress.Text = _currentBeanDiaChi.SoNha + " " + _currentBeanDiaChi.PhuongXa + " " + _currentBeanDiaChi.QuanHuyen + " " + _currentBeanDiaChi.TinhThanh;
                }
            }
            _currentmoney = CountTotalMoney(_lstCart);
            _tvPrice.Text = String.Format("{0:#,0}", _currentmoney) + " VNĐ";
            if (_currentship == -1)
            {
                _tvShip.Text = "Không giao hàng";
                _tvShip.SetTextColor(Color.Red);
            }
            else
            {
                _tvShip.Text = String.Format("{0:#,0}", _currentship) + " VNĐ";
                _tvShip.SetTextColor(Color.Black);
            }
        }
        private float CountTotalMoney(List<BeanShoppingCart> data)
        {
            float _totalMoney = 0;
            List<BeanMonAn> _lstSelectMonAn = new List<BeanMonAn>();
            for (int i = 0; i < data.Count; i++)
            {
                BeanMonAn _monAn = SQLiteDataHandler.BeanMonAn_SearchItem(data[i].MaMonAn);
                if (data != null)
                {
                    _lstSelectMonAn.Add(_monAn);
                }
            }
            for (int i = 0; i < data.Count; i++)
            {
                _totalMoney += data[i].SoLuong * _lstSelectMonAn[i].GiaTien;
            }
            return _totalMoney;
        }

        private string generateDistrict(string QuanHuyen)
        {
            var StrDistrictWard = "";
            switch (QuanHuyen)
            {
                case "Q1":
                    StrDistrictWard += "Quận 1";
                    break;
                case "Q2":
                    StrDistrictWard += "Quận 2";
                    break;
                case "Q13":
                    StrDistrictWard += "Quận 3";
                    break;
                case "Q4":
                    StrDistrictWard += "Quận 4";
                    break;
                case "Q5":
                    StrDistrictWard += "Quận 5";
                    break;
                case "Q6":
                    StrDistrictWard += "Quận 6";
                    break;
                case "Q7":
                    StrDistrictWard += "Quận 7";
                    break;
                case "Q8":
                    StrDistrictWard += "Quận 8";
                    break;
                case "Q9":
                    StrDistrictWard += "Quận 9";
                    break;
                case "Q10":
                    StrDistrictWard += "Quận 10";
                    break;
                case "Q11":
                    StrDistrictWard += "Quận 11";
                    break;
                case "Q12":
                    StrDistrictWard += "Quận 12";
                    break;
                case "QBT":
                    StrDistrictWard += "Quận Bình Tân";
                    break;
                case "QBTH":
                    StrDistrictWard += "Quận Bình Thạnh";
                    break;
                case "QGV":
                    StrDistrictWard += "Quận Gò Vấp";
                    break;
                case "QPN":
                    StrDistrictWard += "Quận Phú Nhuận";
                    break;
                case "QTB":
                    StrDistrictWard += "Quận Tân Bình";
                    break;
                case "QTP":
                    StrDistrictWard += "Quận Tân Phú";
                    break;
                case "QTD":
                    StrDistrictWard += "Quận Thủ Đức";
                    break;
                case "HBC":
                    StrDistrictWard += "Huyện Bình Chánh";
                    break;
                case "HCG":
                    StrDistrictWard += "Huyện Cần Giờ";
                    break;
                case "HCC":
                    StrDistrictWard += "Huyện Củ Chi";
                    break;
                case "HHM":
                    StrDistrictWard += "Huyện Hóc Môn";
                    break;
                case "HNB":
                    StrDistrictWard += "Huyện nhà bè";
                    break;
            }
            return StrDistrictWard;
        }
    }
}
