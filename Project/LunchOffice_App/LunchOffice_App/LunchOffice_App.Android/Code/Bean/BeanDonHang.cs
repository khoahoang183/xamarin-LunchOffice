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
    public class BeanDonHang
    {
        public BeanDonHang()
        {
        }

        public BeanDonHang(int maDonHang, string maKH, string diaChi, string soDT, int hinhThuThanhToan, string maGiamGia, float phiVanChuyen, float thanhTien, int trangThai, int created, int createdBy)
        {
            MaDonHang = maDonHang;
            MaKH = maKH;
            DiaChi = diaChi;
            SoDT = soDT;
            HinhThuThanhToan = hinhThuThanhToan;
            MaGiamGia = maGiamGia;
            PhiVanChuyen = phiVanChuyen;
            ThanhTien = thanhTien;
            TrangThai = trangThai;
            Created = created;
            CreatedBy = createdBy;
        }

        public int MaDonHang { get; set; }

        public string MaKH { get; set; }

        public string DiaChi { get; set; }

        public string SoDT { get; set; }

        public int HinhThuThanhToan { get; set; }

        public string MaGiamGia { get; set; }

        public float PhiVanChuyen { get; set; }

        public float ThanhTien { get; set; }

        public int TrangThai { get; set; }

        public int Created { get; set; }

        public int CreatedBy { get; set; }
    }
}