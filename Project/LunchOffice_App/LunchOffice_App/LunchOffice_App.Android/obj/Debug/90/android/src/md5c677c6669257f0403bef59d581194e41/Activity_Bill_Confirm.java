package md5c677c6669257f0403bef59d581194e41;


public class Activity_Bill_Confirm
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("LunchOffice_App.Droid.Code.Activities.Activity_Bill_Confirm, LunchOffice_App.Android", Activity_Bill_Confirm.class, __md_methods);
	}


	public Activity_Bill_Confirm ()
	{
		super ();
		if (getClass () == Activity_Bill_Confirm.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Activities.Activity_Bill_Confirm, LunchOffice_App.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
