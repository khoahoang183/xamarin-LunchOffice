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
    public class BeanMonAn
    {
        public BeanMonAn(){ }

        public BeanMonAn(int maMon, string tenMon, int maLoai, string mieuTa, string hinhAnh, float giaTien, int khoaMon, DateTime? created, DateTime? modified, string createdBy, string modifiedBy)
        {
            MaMon = maMon;
            TenMon = tenMon;
            MaLoai = maLoai;
            MieuTa = mieuTa;
            HinhAnh = hinhAnh;
            GiaTien = giaTien;
            KhoaMon = khoaMon;
            Created = created;
            Modified = modified;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
        }

        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public int MaLoai { get; set; }
        public string MieuTa { get; set; }
        public string HinhAnh { get; set; }
        public float GiaTien { get; set; }
        public int KhoaMon { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

    }
}