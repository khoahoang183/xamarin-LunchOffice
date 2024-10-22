﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using ModernHttpClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LunchOffice_App.Droid.Code.Utilities
{
    public static class Utilities_API
    {
        public static string _SiteName = "https://87545982.ngrok.io";
        public static string _SiteImageUrl = "/Assets/Images/HinhMonAn/";
        public static string _APIDetailName = ""; // api url
        public static string _JsonResult = "";
        public static BeanAPI _BeanAPIResult = null;
        public static HttpClient client = null;

        public static List<BeanMonAn> _lstMonAn = new List<BeanMonAn>();
        public static BeanNguoiDung RESULT_APILOGIN_BEANNGUOIDUNG = new BeanNguoiDung();
        public static BeanNguoiDung RESULT_APICONFIRMOTP_BEANNGUOIDUNG = new BeanNguoiDung();
        public static BeanNguoiDung RESULT_APIREGISTER_BEANNGUOIDUNG = new BeanNguoiDung();
        public static List<BeanDonHang> RESULT_APIGET_LISTDONHANG = new List<BeanDonHang>();
        public static List<BeanDiaChi> RESULT_APIGET_LISTDIACHI_BYMANGUOIDUNG = new List<BeanDiaChi>();
        public static bool RESULT_APIADD_DIACHI = false;
        public static bool RESULT_APIADD_BILL = false;
        public static float RESULT_API_COUNTSHIP = -1;
        public static bool RESULT_APIOTP_BOOL;
        public static async Task<BeanAPI> ConsumeAPI(string uri)
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                }
                return null;
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }

        /// <summary>
        /// Neu thanh cong -> List BeanMonAn, that bai -> null
        /// </summary>
        /// <returns></returns>
        public static async Task API_GetListMonAn()
        {
            try
            {
                _APIDetailName = "/Link/ApiMon.ashx?func=select";
                _BeanAPIResult = new BeanAPI();
                string API = _SiteName + _APIDetailName;
                _BeanAPIResult = await Utilities_API.ConsumeAPI(API);
                if (_BeanAPIResult.status.Equals("SUCCESS"))
                {
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
                        _lstMonAn = JsonConvert.DeserializeObject<List<BeanMonAn>>(_BeanAPIResult.data);
                    }
                }
                //return _lstMonAn;
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// Neu thanh cong -> List BeanDiaChi, that bai -> null
        /// </summary>
        /// <returns></returns>
        public static async Task API_GetListDiaChiByMaNguoiDung(string MaNguoiDung)
        {
            try
            {
                //https://13e6a3c5.ngrok.io/Link/ApiDiaChi.ashx?func=select&data={%22MaNguoiDung%22:%22e605743b-314a-48d9-b292-bc46d7880bf4%22}
                RESULT_APIGET_LISTDIACHI_BYMANGUOIDUNG = new List<BeanDiaChi>();
                _APIDetailName = "/Link/ApiDiaChi.ashx?func=select";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);
                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{MaNguoiDung:\""+MaNguoiDung+"\"}")
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                        {
                            RESULT_APIGET_LISTDIACHI_BYMANGUOIDUNG = JsonConvert.DeserializeObject<List<BeanDiaChi>>(_BeanAPIResult.data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Neu thanh cong -> BeanNguoiDung, that bai -> null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task API_GetLogIn(string id, string password)
        {
            try
            {
                //http://localhost:63211/Link/ApiNguoiDung.ashx?func=signin&data={TaiKhoan:%22user1%22,MatKhau:%22123456%22}
                _APIDetailName = "/Link/ApiNguoiDung.ashx?func=signin";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{TaiKhoan:\""+id+"\", MatKhau:\""+password+"\"}")
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                        {
                            RESULT_APILOGIN_BEANNGUOIDUNG = JsonConvert.DeserializeObject<BeanNguoiDung>(_BeanAPIResult.data);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Neu thanh cong -> List BeanDonHang, that bai -> null
        /// </summary>
        /// <returns></returns>
        public static async Task API_GetListDonHang(string id)
        {
            try
            {
                _APIDetailName = "/Link/ApiDonHang.ashx?func=select";
                _BeanAPIResult = new BeanAPI();
                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{TaiKhoan:"+id+"}")
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                        {
                            RESULT_APIGET_LISTDONHANG = JsonConvert.DeserializeObject<List<BeanDonHang>>(_BeanAPIResult.data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Neu thanh cong -> MaNguoiDung, that bai -> null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task API_GetRegister(string TaiKhoan, string Email, string password)
        {
            try
            {
                RESULT_APIREGISTER_BEANNGUOIDUNG = new BeanNguoiDung();
                //http://c3da38bf.ngrok.io/Link/ApiNguoiDung.ashx?func=signup&data={TaiKhoan:khoatest,Email:hoangdangkhoa.m9@gmail.com,MatKhau:Aa123456}
                _APIDetailName = "/Link/ApiNguoiDung.ashx?func=signup";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{TaiKhoan:\""+TaiKhoan+"\"," +
                                                             "MatKhau:\""+password+"\"," +
                                                             "Email:\""+Email+"\"}"
                                                     )
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                        {
                            List<BeanNguoiDung> temp = JsonConvert.DeserializeObject<List<BeanNguoiDung>>(_BeanAPIResult.data);
                            if (temp.Count > 0)
                            {
                                RESULT_APIREGISTER_BEANNGUOIDUNG = temp[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Neu thanh cong -> OK, that bai -> NO
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task API_ConfirmOTP(string MaNguoiDung, string OTP)
        {
            //http://localhost:63211/Link/ApiNguoiDung.ashx?func=authentication&data={MaNguoiDung:1,MaOTP:1}
            try
            {
                RESULT_APIOTP_BOOL = false;
                _APIDetailName = "/Link/ApiNguoiDung.ashx?func=authentication";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{MaNguoiDung:\""+MaNguoiDung+"\", MaOTP:\""+OTP+"\"}")
                });

                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        RESULT_APIOTP_BOOL = true;
                        RESULT_APICONFIRMOTP_BEANNGUOIDUNG = JsonConvert.DeserializeObject<BeanNguoiDung>(_BeanAPIResult.data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Neu thanh cong -> true, that bai -> false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task API_UpdateUser(BeanNguoiDung user)
        {
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
{
                    new KeyValuePair<string, string>("Data", "{"+JsonConvert.SerializeObject(user)+"}")
                });

                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        //_result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Neu thanh cong -> true, that bai -> false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task API_Order(BeanDonHang beanDonHang, List<BeanItemCart> lstGioHang)
        {
            try
            {
                //beanDonHang = new BeanDonHang();
                //beanDonHang.MaKH = "e605743b-314a-48d9-b292-bc46d7880bf4";
                //beanDonHang.DiaChi = "31B Sư Vạn Hạnh Phường 3 Quận 10 Tp.Hồ Chí Minh";
                //beanDonHang.PhiVanChuyen = 15000;
                //beanDonHang.ThanhTien = 65000;

                //lstGioHang = new List<BeanItemCart>();
                //BeanItemCart temp = new BeanItemCart();
                //temp.MaMon = 2;
                //temp.TenMon = "Cơm Sườn Nướng";
                //temp.GiaTien = 50000;
                //temp.HinhAnh = "ComSuonNuong.jpeg";
                //temp.SoLuong = 1;
                //lstGioHang.Add(temp);

                //Link/ApiDonHang.ashx?func=update
                RESULT_APIADD_BILL = false;

                _APIDetailName = "/Link/ApiDonHang.ashx?func=update";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);
                string jsonBDonHang = JsonConvert.SerializeObject(beanDonHang);
                string jsonBCTDonHang = JsonConvert.SerializeObject(lstGioHang);
                var jsonData = new FormUrlEncodedContent(new[]
{
                    new KeyValuePair<string, string>("Data", "{\"jsonBDonHang\":"+jsonBDonHang+",\"jsonBCTDonHang\":"+jsonBCTDonHang+"}")
                });

                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        RESULT_APIADD_BILL = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task<BeanNguoiDung> API_Test(string email, string password)
        {
            BeanNguoiDung _resultNguoiDung = null;
            try
            {
                _APIDetailName = "/Link/ApiMon.ashx?func=select";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{MaMon:2,}")
                });

                //HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {

                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
                        _resultNguoiDung = new BeanNguoiDung();
                        _resultNguoiDung = JsonConvert.DeserializeObject<BeanNguoiDung>(_BeanAPIResult.data);
                    }
                }
                return _resultNguoiDung;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task API_AddNewAddress(string MaDiaChi, string MaNguoiDung, string HoTen, string SoDT, string TinhThanh, string QuanHuyen, string PhuongXa, string SoNha, string MacDinh)
        {
            try
            {
                RESULT_APIADD_DIACHI = false;
                //{"MaDiaChi":0,"MaNguoiDung":"e605743b-314a-48d9-b292-bc46d7880bf4","HoTen":"Lý Bá Đông","SoDT":"0764553313","TinhThanh":"Tp.Hồ Chí Minh",
                //"QuanHuyen":"Q5","PhuongXa":"P5","SoNha":"312 Nguyễn Thượng Hiền ","MacDinh":false}
                MaDiaChi = "0";
                MaNguoiDung = "fbf30094-7783-4158-b1f1-34a5219cfb01";
                HoTen = "Đỗ Thảo";
                SoDT = "0123456789";
                TinhThanh = "Tp.Hồ Chí Minh";
                QuanHuyen = "Q10";
                PhuongXa = "P5";
                SoNha = "312 Nguyễn Thượng Hiền";
                MacDinh = "true";
                _APIDetailName = "/Link/ApiDiaChi.ashx?func=update";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{MaDiaChi:\""+MaDiaChi+"\"," +
                                                             "MaNguoiDung:\""+MaNguoiDung+"\"," +
                                                             "HoTen:\""+HoTen+"\"," +
                                                             "SoDT:\""+SoDT+"\"," +
                                                             "TinhThanh:\""+TinhThanh+"\"," +
                                                             "QuanHuyen:\""+QuanHuyen+"\"," +
                                                             "PhuongXa:\""+PhuongXa+"\"," +
                                                             "SoNha:\""+SoNha+"\"," +
                                                             "MacDinh:\""+MacDinh+"\"}"
                                                     )
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        RESULT_APIADD_DIACHI = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public static async Task API_GetShipFee(string District)
        {
            try
            {
                //http://localhost:63211/Link/ApiDiaChi.ashx?func=calculatefee&data={%22District%22:%22qu%E1%BA%ADn%2010%22}
                RESULT_API_COUNTSHIP = -1;

                _APIDetailName = "/Link/ApiDiaChi.ashx?func=calculatefee";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                var jsonData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("Data", "{District:\""+District+"\"}")
                });
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, jsonData);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                        {
                            var resultobject = new { fee = "" };
                            resultobject = JsonConvert.DeserializeAnonymousType(_BeanAPIResult.data, resultobject);
                            RESULT_API_COUNTSHIP = float.Parse(resultobject.fee);
                            if (RESULT_API_COUNTSHIP > 100000 || RESULT_API_COUNTSHIP == -10)
                            {
                                RESULT_API_COUNTSHIP = RESULT_API_COUNTSHIP / 10;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


    }
}


