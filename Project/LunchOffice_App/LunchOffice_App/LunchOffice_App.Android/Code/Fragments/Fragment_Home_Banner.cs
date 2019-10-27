using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LunchOffice_App.Droid.Code.Fragments
{
    public class Fragment_Home_Banner : Fragment
    {
        ImageView _imgBanner;
        int _img;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        public Fragment_Home_Banner(int _img)
        {
            this._img = _img;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.Layout_Fragment_Home_Banner, container, false);
            _imgBanner = root.FindViewById<ImageView>(Resource.Id.FragmentHome_imgBanner);
            if (_img == 1)
            {
                _imgBanner.SetBackgroundResource(Resource.Drawable.img_banner001);
            }
            else if (_img == 2)
            {
                _imgBanner.SetBackgroundResource(Resource.Drawable.img_banner002);
            }
            else if (_img == 3)
            {
                _imgBanner.SetBackgroundResource(Resource.Drawable.img_banner003);
            }
            return root;
        }
    }
}