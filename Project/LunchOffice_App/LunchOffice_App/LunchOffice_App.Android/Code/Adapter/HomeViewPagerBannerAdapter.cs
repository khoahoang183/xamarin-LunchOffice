using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using LunchOffice_App.Droid.Code.Fragments;

namespace LunchOffice_App.Droid.Code.Adapter
{
    public class HomeViewPagerBannerAdapter : FragmentPagerAdapter
    {
        private List<Android.Support.V4.App.Fragment> _listFragment = new List<Android.Support.V4.App.Fragment>();

        public HomeViewPagerBannerAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            //_listFragment.Add(new Fragment_Home_Banner(1));
        }
        public override int Count => _listFragment.Count();

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _listFragment[position];
        }
    }
}