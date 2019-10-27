package md53365aff7e0b136a678f1bfb967cb0555;


public class HomeRecyclerViewAdapter_MyViewHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("LunchOffice_App.Droid.Code.Adapter.HomeRecyclerViewAdapter+MyViewHolder, LunchOffice_App.Android", HomeRecyclerViewAdapter_MyViewHolder.class, __md_methods);
	}


	public HomeRecyclerViewAdapter_MyViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == HomeRecyclerViewAdapter_MyViewHolder.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Adapter.HomeRecyclerViewAdapter+MyViewHolder, LunchOffice_App.Android", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
