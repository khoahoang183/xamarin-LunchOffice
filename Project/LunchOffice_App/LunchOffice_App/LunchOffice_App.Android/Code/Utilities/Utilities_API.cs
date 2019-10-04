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
namespace LunchOffice_App.Droid.Code.Utilities
{
    public static class Utilities_API
    {
        public static string _JsonResult = "";

        /// <summary>
        /// uri example: "http://www.pizzaboy.de/app/pizzaboy.json"
        /// </summary>
        /// <param name="Uri"></param>
        /// <returns></returns>
        public static async Task ConsumeAPI(string uri)
        {
            if(!_JsonResult.Equals(""))
            {
                _JsonResult = "";
            }
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    _JsonResult = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}