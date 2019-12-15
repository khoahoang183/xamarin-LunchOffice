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
using LunchOffice_App.Droid.Code.SQLite;
using static Android.Support.V7.Widget.RecyclerView;

namespace LunchOffice_App.Droid.Code.Adapter
{
    class ShoppingCartRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<BeanShoppingCart> _lstData = new List<BeanShoppingCart>();
        public event EventHandler<int> ItemClick;
        public event EventHandler<int> ClickIncrease;
        public event EventHandler<int> ClickDecrease;
        private Context _context = null;
        private BeanMonAn _monAn = new BeanMonAn();
        public ShoppingCartRecyclerViewAdapter(Context context, List<BeanShoppingCart> _lstData)
        {
            this._context = context;
            this._lstData = _lstData;
        }
        public override int ItemCount => _lstData.Count();

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View _recyRow = inflater.Inflate(Resource.Layout.Item_GioHang_RecyclerView, parent, false);
            return new CartHolder(_recyRow, OnClick, Click_Increase, Click_Decrease);
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            _monAn = SQLiteDataHandler.BeanMonAn_SearchItem(_lstData[position].MaMonAn);
            if (_monAn != null)
            {
                CartHolder recyclerViewHolder = holder as CartHolder;
                recyclerViewHolder._tvDishName.Text = _monAn.TenMon;
                recyclerViewHolder._tvDishPrice.Text = String.Format("{0:#,0}", _monAn.GiaTien) + " VNĐ";
                Utilities.Utilities_LoadImage.LoadImageToImageView(_monAn.HinhAnh, recyclerViewHolder._imgDish);
                recyclerViewHolder._tvCount.Text = _lstData[position].SoLuong.ToString();
            }          
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
            {
                ItemClick(this, obj);
            }
        }
        private void Click_Increase(int obj)
        {
            if (ClickIncrease != null)
            {
                ClickIncrease(this, obj);
            }
        }
        private void Click_Decrease(int obj)
        {
            if (ClickDecrease != null)
            {
                ClickDecrease(this, obj);
            }
        }

    }
    public class CartHolder : ViewHolder
    {
        public View _ViewHolder { get; set; }
        public ImageView _imgDish { get; set; }
        public TextView _tvDishName { get; set; }
        public TextView _tvCount { get; set; }
        public TextView _tvDishPrice { get; set; }
        public LinearLayout _lnContent { get; set; }
        public Button _btnIncrease { get; set; }
        public Button _btnDecrease { get; set; }
        public ImageView _imgDeleteItem { get; set; }
        
        public CartHolder(View itemView, Action<int> click_delItem, Action<int> increase, Action<int> decrease) : base(itemView)
        {
            _ViewHolder = itemView;
            _imgDish = itemView.FindViewById<ImageView>(Resource.Id.ItemGioHang_imgDish);
            _tvDishName = itemView.FindViewById<TextView>(Resource.Id.ItemGioHang_tvDishName);
            _tvDishPrice = itemView.FindViewById<TextView>(Resource.Id.ItemGioHang_tvDishPrice);
            _tvCount = itemView.FindViewById<TextView>(Resource.Id.tv_ItemGioHang_Count);
            _lnContent = itemView.FindViewById<LinearLayout>(Resource.Id.ItemGioHang_linearContent);
            _btnIncrease = itemView.FindViewById<Button>(Resource.Id.btn_ItemGioHang_btnIncrease);
            _btnDecrease = itemView.FindViewById<Button>(Resource.Id.btn_ItemGioHang_btnDecrease);

            //itemView.Click += (sender, e) => listener(base.Position);
            _btnIncrease.Click += (sender, e) => increase(base.Position);
            _btnDecrease.Click += (sender, e) => decrease(base.Position);
            _imgDeleteItem = itemView.FindViewById<ImageView>(Resource.Id.ItemGioHang_imgDeleteItem);
            _imgDeleteItem.Click += (sender, e) => click_delItem(base.Position);
        }
    }
}