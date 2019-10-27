package md5d455a9edea21171133419489d36f3762;


public class Activity_Login
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("LunchOffice_App.Droid.Activities.Activity_Login, LunchOffice_App.Android", Activity_Login.class, __md_methods);
	}


	public Activity_Login ()
	{
		super ();
		if (getClass () == Activity_Login.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Activities.Activity_Login, LunchOffice_App.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
