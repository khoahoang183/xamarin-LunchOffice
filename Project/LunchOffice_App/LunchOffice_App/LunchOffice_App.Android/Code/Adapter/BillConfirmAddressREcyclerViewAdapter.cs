using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using static Android.Support.V7.Widget.RecyclerView;

namespace LunchOffice_App.Droid.Code.Adapter
{
    class BillConfirmAddressRecyclerViewAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> Click_Choose;
        public event EventHandler<int> Click_Edit;
        public event EventHandler<int> Click_Delete;
        List<BeanDiaChi> _lstDiaChi = new List<BeanDiaChi>();
        int _index = -1;
        private void ClickChoose(int obj)
        {
            if (Click_Choose != null)
            {
                Click_Choose(this, obj);
            }
        }
        private void ClickEdit(int obj)
        {
            if (Click_Edit != null)
            {
                Click_Edit(this, obj);
            }
        }
        private void ClickDelete(int obj)
        {
            if (Click_Delete != null)
            {
                Click_Delete(this, obj);
            }
        }
        public override int ItemCount => _lstDiaChi.Count;
        public BillConfirmAddressRecyclerViewAdapter(List<BeanDiaChi> lstDiaChi, int index)
        {
            this._lstDiaChi = lstDiaChi;
            this._index = index;
        }
        public override int GetItemViewType(int position)
        {
            if (_lstDiaChi[position].MacDinh == true)
            {
                return 1;
            }
            return 0;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View _recyRow = inflater.Inflate(Resource.Layout.Item_Bill_Confirm_Change_Address, parent, false);

            AddressViewHolder row = new AddressViewHolder(_recyRow, ClickChoose, ClickEdit, ClickDelete);
            if (viewType == 1)
            {
                row._tvName.SetTextColor(Android.Graphics.Color.ParseColor("#fe8802"));
                row._lnContent.SetBackgroundResource(Resource.Drawable.layout_home_recyclerview_item_shape_3);
            }
            return row;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            AddressViewHolder recyclerViewHolder = holder as AddressViewHolder;
            recyclerViewHolder._tvAddress.Text = recyclerViewHolder._tvName.Text = recyclerViewHolder._tvPhone.Text = " ";
            if (_lstDiaChi[position].MacDinh == true)
            {
                recyclerViewHolder._tvAddress.Text = "Chưa có địa chỉ mặc định!";
                if (!String.IsNullOrEmpty(_lstDiaChi[position].HoTen))
                {
                    recyclerViewHolder._tvName.Text = _lstDiaChi[position].HoTen + " (Mặc định)";
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(_lstDiaChi[position].HoTen))
                {
                    recyclerViewHolder._tvName.Text = _lstDiaChi[position].HoTen;
                }
            }

            if (!String.IsNullOrEmpty(_lstDiaChi[position].SoNha) &&
                !String.IsNullOrEmpty(_lstDiaChi[position].TinhThanh) &&
                !String.IsNullOrEmpty(_lstDiaChi[position].QuanHuyen) &&
                !String.IsNullOrEmpty(_lstDiaChi[position].PhuongXa))
            {
                recyclerViewHolder._tvAddress.Text = "Địa chỉ: " + _lstDiaChi[position].SoNha + " " + _lstDiaChi[position].PhuongXa + " " + _lstDiaChi[position].QuanHuyen + " " + _lstDiaChi[position].TinhThanh;
            }
            if (!String.IsNullOrEmpty(_lstDiaChi[position].SoDT))
            {
                recyclerViewHolder._tvPhone.Text = "Địện thoại: " + _lstDiaChi[position].SoDT;
            }
        }
    }
    public class AddressViewHolder : ViewHolder
    {
        public View _ViewHolder { get; set; }
        public TextView _tvName { get; set; }
        public TextView _tvAddress { get; set; }
        public TextView _tvPhone { get; set; }
        public Button _btnChoose { get; set; }
        public Button _btnEdit { get; set; }
        public Button _btnDelete { get; set; }
        public Button _imgDeleteItem { get; set; }
        public LinearLayout _lnContent { get; set; }

        public AddressViewHolder(View itemView, Action<int> click_choose, Action<int> click_edit, Action<int> click_delete) : base(itemView)
        {
            _ViewHolder = itemView;
            _tvName = itemView.FindViewById<TextView>(Resource.Id.ItemBillAddress_tvName);
            _tvAddress = itemView.FindViewById<TextView>(Resource.Id.ItemBillAddress_tvAddress);
            _tvPhone = itemView.FindViewById<TextView>(Resource.Id.ItemBillAddress_tvPhone);
            _btnChoose = itemView.FindViewById<Button>(Resource.Id.ItemBillAddress_tvShip);
            _btnEdit = itemView.FindViewById<Button>(Resource.Id.ItemBillAddress_btnEdit);
            _btnDelete = itemView.FindViewById<Button>(Resource.Id.ItemBillAddress_tvDelete);
            _lnContent = itemView.FindViewById<LinearLayout>(Resource.Id.ItemBillAddress_linearContent);
            _btnEdit = itemView.FindViewById<Button>(Resource.Id.ItemBillAddress_btnEdit);
            _lnContent.Click += (sender, e) => click_choose(base.Position);

            _btnEdit.Click += (sender, e) => click_edit(base.Position);
            _btnDelete.Click += (sender, e) => click_delete(base.Position);
        }
    }
}