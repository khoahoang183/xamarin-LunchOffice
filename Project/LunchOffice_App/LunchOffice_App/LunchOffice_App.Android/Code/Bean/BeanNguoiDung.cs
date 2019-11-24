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
using SQLite;

namespace LunchOffice_App.Droid.Code.Bean
{
    public class BeanNguoiDung
    {
        [PrimaryKey]
        public string MaNguoiDung { get; set; }

        public string TaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string HoTen { get; set; }

        public bool GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string HinhAnh { get; set; }

        public string Email { get; set; }

        public int LoaiNguoiDung { get; set; }

        public bool KhoaNguoiDung { get; set; }

        public int MaOtp { get; set; }

        public bool KichHoat { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }
        public BeanNguoiDung()
        {

        }

        public BeanNguoiDung(string maNguoiDung, string taiKhoan, string matKhau, string hoTen, bool gioiTinh, DateTime? ngaySinh, string hinhAnh, string email, int loaiNguoiDung, bool khoaNguoiDung, int maOtp, bool kichHoat, DateTime? created, DateTime? modified, string createdBy, string modifiedBy)
        {
            MaNguoiDung = maNguoiDung;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            HinhAnh = hinhAnh;
            Email = email;
            LoaiNguoiDung = loaiNguoiDung;
            KhoaNguoiDung = khoaNguoiDung;
            MaOtp = maOtp;
            KichHoat = kichHoat;
            Created = created;
            Modified = modified;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
        }
    }
}