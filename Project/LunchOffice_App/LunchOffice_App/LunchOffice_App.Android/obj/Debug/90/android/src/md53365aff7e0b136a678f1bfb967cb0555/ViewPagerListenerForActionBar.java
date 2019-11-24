package md53365aff7e0b136a678f1bfb967cb0555;


public class ViewPagerListenerForActionBar
	extends android.support.v4.view.ViewPager.SimpleOnPageChangeListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LunchOffice_App.Droid.Code.Adapter.ViewPagerListenerForActionBar, LunchOffice_App.Android", ViewPagerListenerForActionBar.class, __md_methods);
	}


	public ViewPagerListenerForActionBar ()
	{
		super ();
		if (getClass () == ViewPagerListenerForActionBar.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Adapter.ViewPagerListenerForActionBar, LunchOffice_App.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
