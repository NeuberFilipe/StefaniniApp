package md5393e3d3bc8e75fbf03fd2bacdcde063d;


public class CitiesView
	extends mvvmcross.droid.support.v7.appcompat.MvxAppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Stefanini.Xamarin.Droid.Views.CitiesView, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CitiesView.class, __md_methods);
	}


	public CitiesView ()
	{
		super ();
		if (getClass () == CitiesView.class)
			mono.android.TypeManager.Activate ("Stefanini.Xamarin.Droid.Views.CitiesView, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
