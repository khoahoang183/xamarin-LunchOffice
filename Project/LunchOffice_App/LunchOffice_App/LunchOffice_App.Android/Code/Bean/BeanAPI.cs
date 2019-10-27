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
    public class BeanAPI
    {
        public string status { get; set; }
        public Mess mess { get; set; }
        public string data { get; set; }
        public string created { get; set; }
        public class Mess
        {
            public object Key { get; set; }
            public object Value { get; set; }
        }

        public BeanAPI(string status, Mess mess, string data, string created)
        {
            this.status = status;
            this.mess = mess;
            this.data = data;
            this.created = created;
        }

        public BeanAPI()
        {
        }
    }
}