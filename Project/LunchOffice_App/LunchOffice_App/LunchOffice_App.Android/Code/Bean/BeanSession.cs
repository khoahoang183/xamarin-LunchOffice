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
    public class BeanSession
    {
        public string MaNguoiDung { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }

        public BeanSession()
        {

        }
        public BeanSession(string maNguoiDung, string taiKhoan, string matKhau)
        {
            MaNguoiDung = maNguoiDung;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
        }
    }
}