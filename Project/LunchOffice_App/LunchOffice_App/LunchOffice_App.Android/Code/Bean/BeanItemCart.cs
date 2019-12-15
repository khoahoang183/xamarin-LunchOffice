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
    public class BeanItemCart
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public float GiaTien { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
    }
}