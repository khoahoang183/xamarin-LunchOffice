using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace LunchOffice_App.Droid.Code.Activities
{
    [Activity(Label = "Activity_Bill_Confirm_Address", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Activity_Bill_Confirm_Address : Activity
    {
        Button _btnClose, _btnChangeAddress;
        private RecyclerView _recyclerData;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            getLayout();
            // Create your application here
        }
        private void getLayout()
        {
            SetContentView(Resource.Layout.Layout_Bill_Confirm_Change_Address);
            _btnClose = FindViewById<Button>(Resource.Id.BillConfirm_Address_btnClose);
            _btnChangeAddress = FindViewById<Button>(Resource.Id.BillConfirm_Address_btnConfirm);         
            _recyclerData = FindViewById<RecyclerView>(Resource.Id.BillConfirm_Address_recyData);

        }
    }
}