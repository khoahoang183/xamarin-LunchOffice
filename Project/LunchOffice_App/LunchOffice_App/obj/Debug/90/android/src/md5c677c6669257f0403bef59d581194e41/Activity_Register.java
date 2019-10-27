package md5c677c6669257f0403bef59d581194e41;


public class Activity_Register
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
		mono.android.Runtime.register ("LunchOffice_App.Droid.Code.Activities.Activity_Register, LunchOffice_App.Android", Activity_Register.class, __md_methods);
	}


	public Activity_Register ()
	{
		super ();
		if (getClass () == Activity_Register.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Activities.Activity_Register, LunchOffice_App.Android", "", this, new java.lang.Object[] {  });
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
