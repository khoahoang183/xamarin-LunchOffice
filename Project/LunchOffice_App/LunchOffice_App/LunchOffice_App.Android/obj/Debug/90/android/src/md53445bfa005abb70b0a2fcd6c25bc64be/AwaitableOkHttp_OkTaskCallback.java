package md53445bfa005abb70b0a2fcd6c25bc64be;


public class AwaitableOkHttp_OkTaskCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.squareup.okhttp.Callback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFailure:(Lcom/squareup/okhttp/Request;Ljava/io/IOException;)V:GetOnFailure_Lcom_squareup_okhttp_Request_Ljava_io_IOException_Handler:OkHttp.ICallbackInvoker, OkHttp\n" +
			"n_onResponse:(Lcom/squareup/okhttp/Response;)V:GetOnResponse_Lcom_squareup_okhttp_Response_Handler:OkHttp.ICallbackInvoker, OkHttp\n" +
			"";
		mono.android.Runtime.register ("ModernHttpClient.AwaitableOkHttp+OkTaskCallback, ModernHttpClient", AwaitableOkHttp_OkTaskCallback.class, __md_methods);
	}


	public AwaitableOkHttp_OkTaskCallback ()
	{
		super ();
		if (getClass () == AwaitableOkHttp_OkTaskCallback.class)
			mono.android.TypeManager.Activate ("ModernHttpClient.AwaitableOkHttp+OkTaskCallback, ModernHttpClient", "", this, new java.lang.Object[] {  });
	}


	public void onFailure (com.squareup.okhttp.Request p0, java.io.IOException p1)
	{
		n_onFailure (p0, p1);
	}

	private native void n_onFailure (com.squareup.okhttp.Request p0, java.io.IOException p1);


	public void onResponse (com.squareup.okhttp.Response p0)
	{
		n_onResponse (p0);
	}

	private native void n_onResponse (com.squareup.okhttp.Response p0);

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
