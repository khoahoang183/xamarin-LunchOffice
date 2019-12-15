using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LunchOffice_App.Droid.Code.Bean
{
    public class BeanCTDonHang
    {
        public int MaCT { get; set; }

        public int MaDonHang { get; set; }

        public int MaMon { get; set; }

        public int SoLuong { get; set; }

        public string GhiChu { get; set; }

        public DateTime? Created { get; set; }

        public string CreatedBy { get; set; }
    }
}