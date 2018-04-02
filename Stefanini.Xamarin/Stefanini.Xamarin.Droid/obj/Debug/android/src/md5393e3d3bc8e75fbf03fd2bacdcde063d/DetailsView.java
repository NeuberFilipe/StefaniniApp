package md5393e3d3bc8e75fbf03fd2bacdcde063d;


public class DetailsView
	extends md5393e3d3bc8e75fbf03fd2bacdcde063d.BaseView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Stefanini.Xamarin.Droid.Views.DetailsView, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DetailsView.class, __md_methods);
	}


	public DetailsView ()
	{
		super ();
		if (getClass () == DetailsView.class)
			mono.android.TypeManager.Activate ("Stefanini.Xamarin.Droid.Views.DetailsView, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
