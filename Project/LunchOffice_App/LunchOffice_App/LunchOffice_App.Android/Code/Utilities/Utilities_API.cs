using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public static string _SiteName = "http://localhost:5000";
        public static string _APIDetailName = ""; // api url
        public static string _JsonResult = "";
        public static BeanAPI _BeanAPIResult = null;
        public static HttpClient client = null;

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
        public static List<BeanMonAn> API_GetListMonAn()
        {
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                List<BeanMonAn> _lstMonAn = new List<BeanMonAn>();
                Task.Run(async () =>
                {
                    _BeanAPIResult = await Utilities_API.ConsumeAPI(_SiteName + _APIDetailName);
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
                        _lstMonAn = JsonConvert.DeserializeObject<List<BeanMonAn>>(_BeanAPIResult.data);
                    }
                });
                return _lstMonAn;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Neu thanh cong -> BeanNguoiDung, that bai -> null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<BeanNguoiDung> API_GetLogIn(string email, string password)
        {
            BeanNguoiDung _resultNguoiDung = new BeanNguoiDung();
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                // content
                JObject json = new JObject(); json.Add("email", email); json.Add("password", password);
                string jsonData = json.ToString();

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, content);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
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

        /// <summary>
        /// Neu thanh cong -> List BeanDonHang, that bai -> null
        /// </summary>
        /// <returns></returns>
        public static List<BeanMonAn> API_GetListDonHang(string id, int startPosition, int endPosition)
        {
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                List<BeanMonAn> _lstMonAn = new List<BeanMonAn>();
                Task.Run(async () =>
                {
                    _BeanAPIResult = await Utilities_API.ConsumeAPI(_SiteName + _APIDetailName);
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
                        _lstMonAn = JsonConvert.DeserializeObject<List<BeanMonAn>>(_BeanAPIResult.data);
                    }
                });
                return _lstMonAn;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Neu thanh cong -> BeanNguoiDung, that bai -> null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<BeanNguoiDung> API_GetRegister(string email, string password)
        {
            BeanNguoiDung _resultNguoiDung = new BeanNguoiDung();
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                // content
                JObject json = new JObject(); json.Add("email", email); json.Add("password", password);
                string jsonData = json.ToString();

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, content);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (!String.IsNullOrEmpty(_BeanAPIResult.data))
                    {
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

        /// <summary>
        /// Neu thanh cong -> OK, that bai -> NO
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> API_ConfirmOTP(string MaNguoiDung, string OTP)
        {
            bool _result = false;
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                // content
                JObject json = new JObject(); json.Add("MaNguoiDung", MaNguoiDung); json.Add("OTP", OTP);
                string jsonData = json.ToString();

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, content);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        _result = true;
                    }
                }
                return _result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Neu thanh cong -> true, that bai -> false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> API_UpdateUser(BeanNguoiDung user)
        {
            bool _result = false;
            try
            {
                _APIDetailName = "/api/values";
                _BeanAPIResult = new BeanAPI();
                client = new HttpClient();
                client.BaseAddress = new Uri(_SiteName);

                // object to json
                string jsonData = JsonConvert.SerializeObject(user);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_APIDetailName, content);
                if (response.IsSuccessStatusCode) // thanh cong -> lay ket qua ve
                {
                    _BeanAPIResult = JsonConvert.DeserializeObject<BeanAPI>(await response.Content.ReadAsStringAsync());
                    if (_BeanAPIResult.status.Equals("SUCCESS"))
                    {
                        _result = true;
                    }
                }
                return _result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Neu thanh cong -> true, that bai -> false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async Task<bool> API_Order()
        {
            return true;
        }

    }
}


