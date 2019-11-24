using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
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
            _listFragment.Add(new Fragment_Home_Banner(1));
            _listFragment.Add(new Fragment_Home_Banner(2));
            _listFragment.Add(new Fragment_Home_Banner(3));
        }
        public override int Count => _listFragment.Count();
        public void AddFragmentView (Func<LayoutInflater,ViewGroup,Bundle,View> view)
        {
            //_listFragment.Add(new Fragment_Home_Banner(view));
            //_listFragment.Add(new Fragment_Home_Banner(view));
        }
        public void AddFragment(Fragment_Home_Banner fragment)
        {
            _listFragment.Add(fragment);
        }
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return _listFragment[position];
        }
    }

    public class ViewPagerListenerForActionBar: ViewPager.SimpleOnPageChangeListener
    {

    }
}