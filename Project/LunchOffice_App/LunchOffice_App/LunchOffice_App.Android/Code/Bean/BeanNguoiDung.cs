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
    public class BeanNguoiDung
    {
        public string MaNguoiDung { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public int GioiTinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public string HinhAnh { get; set; }
        public string DiaChiMacDinh { get; set; }
        public string Email { get; set; }
        public int LoaiNguoiDung { get; set; }
        public int KhoaNguoiDung { get; set; }
        public int MaOTP { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public BeanNguoiDung()
        {

        }

        public BeanNguoiDung(string maNguoiDung, string taiKhoan, string matKhau, string hoTen, int gioiTinh, DateTime ngaySinh, string hinhAnh, string diaChiMacDinh, string email, int loaiNguoiDung, int khoaNguoiDung, int maOTP, DateTime created, DateTime modified, string createdBy, string modifiedBy)
        {
            MaNguoiDung = maNguoiDung;
            TaiKhoan = taiKhoan;
            MatKhau = matKhau;
            HoTen = hoTen;
            GioiTinh = gioiTinh;
            NgaySinh = ngaySinh;
            HinhAnh = hinhAnh;
            DiaChiMacDinh = diaChiMacDinh;
            Email = email;
            LoaiNguoiDung = loaiNguoiDung;
            KhoaNguoiDung = khoaNguoiDung;
            MaOTP = maOTP;
            Created = created;
            Modified = modified;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
        }
    }
}