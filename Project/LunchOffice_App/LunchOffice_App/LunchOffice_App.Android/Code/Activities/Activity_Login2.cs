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

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Login2", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Login2 : Activity
    {
        TextView _tvRegister;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Login_2);
            _tvRegister = FindViewById<TextView>(Resource.Id.tvRegister);
            _tvRegister.Click += tvRegisterClick;
        }

        private void tvRegisterClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activity_Register));
            StartActivity(intent);
        }
    }
}