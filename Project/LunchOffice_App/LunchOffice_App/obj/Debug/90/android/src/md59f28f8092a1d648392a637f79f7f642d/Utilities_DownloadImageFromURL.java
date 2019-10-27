package md59f28f8092a1d648392a637f79f7f642d;


public class Utilities_DownloadImageFromURL
	extends android.os.AsyncTask
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreExecute:()V:GetOnPreExecuteHandler\n" +
			"n_doInBackground:([Ljava/lang/Object;)Ljava/lang/Object;:GetDoInBackground_arrayLjava_lang_Object_Handler\n" +
			"n_onPostExecute:(Ljava/lang/Object;)V:GetOnPostExecute_Ljava_lang_Object_Handler\n" +
			"n_onProgressUpdate:([Ljava/lang/Object;)V:GetOnProgressUpdate_arrayLjava_lang_Object_Handler\n" +
			"";
		mono.android.Runtime.register ("LunchOffice_App.Droid.Code.Utilities.Utilities_DownloadImageFromURL, LunchOffice_App.Android", Utilities_DownloadImageFromURL.class, __md_methods);
	}


	public Utilities_DownloadImageFromURL ()
	{
		super ();
		if (getClass () == Utilities_DownloadImageFromURL.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Utilities.Utilities_DownloadImageFromURL, LunchOffice_App.Android", "", this, new java.lang.Object[] {  });
	}

	public Utilities_DownloadImageFromURL (android.content.Context p0, android.widget.ImageView p1)
	{
		super ();
		if (getClass () == Utilities_DownloadImageFromURL.class)
			mono.android.TypeManager.Activate ("LunchOffice_App.Droid.Code.Utilities.Utilities_DownloadImageFromURL, LunchOffice_App.Android", "Android.Content.Context, Mono.Android:Android.Widget.ImageView, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public void onPreExecute ()
	{
		n_onPreExecute ();
	}

	private native void n_onPreExecute ();


	public java.lang.Object doInBackground (java.lang.Object[] p0)
	{
		return n_doInBackground (p0);
	}

	private native java.lang.Object n_doInBackground (java.lang.Object[] p0);


	public void onPostExecute (java.lang.Object p0)
	{
		n_onPostExecute (p0);
	}

	private native void n_onPostExecute (java.lang.Object p0);


	public void onProgressUpdate (java.lang.Object[] p0)
	{
		n_onProgressUpdate (p0);
	}

	private native void n_onProgressUpdate (java.lang.Object[] p0);

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
