using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Bean;
using LunchOffice_App.Droid.Code.Cmm;
using LunchOffice_App.Droid.Code.SQLite;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Logout", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Logout : Activity
    {
        Button _btnLogout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();


        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Logout);
            _btnLogout = FindViewById<Button>(Resource.Id.Logout_btnLogout);
            _btnLogout.Click += Click_Logout;
        }

        private void Click_Logout(object sender, EventArgs e)
        {
            List<BeanSession> list = new List<BeanSession>();
            list = SQLiteDataHandler.BeanSession_LoadList();
            if (list != null && list.Count >0)
            {
                // xoa gio hang 
                CmmVar.LIST_SHOPPING_CART = new List<BeanShoppingCart>();
                SQLiteDataHandler.BeanSession_ClearSession();
                Intent intent = new Intent(this, typeof(Activity_Home));
                StartActivity(intent);
            }
        }
    }
}