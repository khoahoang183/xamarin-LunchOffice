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
        List<BeanDiaChi> _lstDiaChi = new List<BeanDiaChi>();
        public event EventHandler<int> ItemClick;
        public override int ItemCount => _lstDiaChi.Count;
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View _recyRow = inflater.Inflate(Resource.Layout.Item_Bill_Confirm_Change_Address, parent, false);
            return new MyViewHolder(_recyRow, OnClick);
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

        }

        private void Item_Click(object sender, EventArgs e)
        {

        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
            {
                ItemClick(this, obj);
            }
        }
    }
    public class AddressViewHolder : ViewHolder
    {
        public View _ViewHolder { get; set; }

        public AddressViewHolder(View itemView, Action<int> listener) : base(itemView)
        {
            _ViewHolder = itemView;

            itemView.Click += (sender, e) => listener(base.Position);
        }
    }
}