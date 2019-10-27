﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Activities;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Utilities;
using static Android.Support.V7.Widget.RecyclerView;

namespace LunchOffice_App.Droid.Code.Adapter
{
    public partial class HomeRecyclerViewAdapter : RecyclerView.Adapter
    {
        public Context _context = null;
        public List<BeanMonAn> _listMonAn = null;
        int _type = 0; // 0 - all, 123 - category 123

        public HomeRecyclerViewAdapter(Context context, List<BeanMonAn> listMonAn, int type)
        {
            this._context = context;
            this._listMonAn = listMonAn;
            this._type = type;
        }
        public override int ItemCount => _listMonAn.Count();
        public class MyViewHolder : ViewHolder
        {
            public View _ViewHolder { get; set; }
            public ImageView _imgDish { get; set; }
            public TextView _tvDishName { get; set; }
            public TextView _tvDishCategory { get; set; }
            public TextView _tvDishPrice { get; set; }
            public LinearLayout _lnContent { get; set; }
            public MyViewHolder(View itemView) : base(itemView)
            {
                _ViewHolder = itemView;
                _imgDish = itemView.FindViewById<ImageView>(Resource.Id.ItemHomeRecy_imgDish);
                _tvDishName = itemView.FindViewById<TextView>(Resource.Id.ItemHomeRecy_tvDishName);
                _tvDishCategory = itemView.FindViewById<TextView>(Resource.Id.ItemHomeRecy_tvDishCategory);
                _tvDishPrice = itemView.FindViewById<TextView>(Resource.Id.ItemHomeRecy_tvDishPrice);
                _lnContent = itemView.FindViewById<LinearLayout>(Resource.Id.ItemHomeRecy_linearContent);
            }
        }
        public override ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context);
            View _recyRow = inflater.Inflate(Resource.Layout.Item_Home_RecyclerView, parent, false);
            return new MyViewHolder(_recyRow);
        }
        public override void OnBindViewHolder(ViewHolder holder, int position)
        {
            MyViewHolder recyclerViewHolder = holder as MyViewHolder;
            recyclerViewHolder._tvDishName.Text = _listMonAn[position].TenMon;
            recyclerViewHolder._tvDishPrice.Text = _listMonAn[position].GiaTien.ToString()+"đ";
            switch (_listMonAn[position].MaLoai)
            {
                case 1:
                    {
                        recyclerViewHolder._tvDishCategory.Text = "Món cơm";
                        break;
                    }
                case 2:
                    {
                        recyclerViewHolder._tvDishCategory.Text = "Món nước";
                        break;
                    }
                case 3:
                    {
                        recyclerViewHolder._tvDishCategory.Text = "Giải khát";
                        break;
                    }
            }
            // load image
            Utilities.Utilities_LoadImage.LoadImageToImageView(_listMonAn[position].HinhAnh, recyclerViewHolder._imgDish);
            recyclerViewHolder._lnContent.Click += delegate
            {
                Intent intent = new Intent(_context, typeof(Activity_Food_Detail));
                intent.PutExtra("Detail_MaMon", _listMonAn[position].MaMon.ToString());
                string a = _listMonAn[position].HinhAnh;
                _context.StartActivity(intent);
                return;
            };
        }
        
    }
}