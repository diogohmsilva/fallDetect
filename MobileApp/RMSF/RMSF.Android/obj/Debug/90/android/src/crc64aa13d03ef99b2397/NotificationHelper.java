package crc64aa13d03ef99b2397;


public class NotificationHelper
	extends android.app.Notification
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("RMSF.Droid.NotificationHelper, RMSF.Android", NotificationHelper.class, __md_methods);
	}


	public NotificationHelper ()
	{
		super ();
		if (getClass () == NotificationHelper.class)
			mono.android.TypeManager.Activate ("RMSF.Droid.NotificationHelper, RMSF.Android", "", this, new java.lang.Object[] {  });
	}


	public NotificationHelper (android.os.Parcel p0)
	{
		super (p0);
		if (getClass () == NotificationHelper.class)
			mono.android.TypeManager.Activate ("RMSF.Droid.NotificationHelper, RMSF.Android", "Android.OS.Parcel, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public NotificationHelper (int p0, java.lang.CharSequence p1, long p2)
	{
		super (p0, p1, p2);
		if (getClass () == NotificationHelper.class)
			mono.android.TypeManager.Activate ("RMSF.Droid.NotificationHelper, RMSF.Android", "System.Int32, mscorlib:Java.Lang.ICharSequence, Mono.Android:System.Int64, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
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
