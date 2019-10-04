using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Activities;

namespace LunchOffice_App.Droid.Activities
{
    [Activity(Label = "Lunch Office",Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]


    public class Activity_Login : Activity
    {
        private Button _btnLogin, _btnNoLogin;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();

        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Login);
            _btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            _btnNoLogin = FindViewById<Button>(Resource.Id.btnNoLogin);
            _btnLogin.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Activity_Login2));
                StartActivity(intent);
            };
        }

    }
}