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
    public class BeanDiaChi
    {

        public int MaDiaChi { get; set; }

        public string MaNguoiDung { get; set; }

        public string HoTen { get; set; }

        public string SoDT { get; set; }

        public string TinhThanh { get; set; }

        public string QuanHuyen { get; set; }

        public string PhuongXa { get; set; }

        public string SoNha { get; set; }

        public bool MacDinh { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
    }
}