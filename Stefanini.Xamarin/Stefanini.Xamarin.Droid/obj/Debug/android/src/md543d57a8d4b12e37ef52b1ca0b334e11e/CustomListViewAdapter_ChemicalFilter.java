package md543d57a8d4b12e37ef52b1ca0b334e11e;


public class CustomListViewAdapter_ChemicalFilter
	extends android.widget.Filter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_performFiltering:(Ljava/lang/CharSequence;)Landroid/widget/Filter$FilterResults;:GetPerformFiltering_Ljava_lang_CharSequence_Handler\n" +
			"n_publishResults:(Ljava/lang/CharSequence;Landroid/widget/Filter$FilterResults;)V:GetPublishResults_Ljava_lang_CharSequence_Landroid_widget_Filter_FilterResults_Handler\n" +
			"";
		mono.android.Runtime.register ("Stefanini.Xamarin.Droid.Resources.CustomListViewAdapter+ChemicalFilter, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomListViewAdapter_ChemicalFilter.class, __md_methods);
	}


	public CustomListViewAdapter_ChemicalFilter ()
	{
		super ();
		if (getClass () == CustomListViewAdapter_ChemicalFilter.class)
			mono.android.TypeManager.Activate ("Stefanini.Xamarin.Droid.Resources.CustomListViewAdapter+ChemicalFilter, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public CustomListViewAdapter_ChemicalFilter (md543d57a8d4b12e37ef52b1ca0b334e11e.CustomListViewAdapter p0)
	{
		super ();
		if (getClass () == CustomListViewAdapter_ChemicalFilter.class)
			mono.android.TypeManager.Activate ("Stefanini.Xamarin.Droid.Resources.CustomListViewAdapter+ChemicalFilter, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Stefanini.Xamarin.Droid.Resources.CustomListViewAdapter, Stefanini.Xamarin.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public android.widget.Filter.FilterResults performFiltering (java.lang.CharSequence p0)
	{
		return n_performFiltering (p0);
	}

	private native android.widget.Filter.FilterResults n_performFiltering (java.lang.CharSequence p0);


	public void publishResults (java.lang.CharSequence p0, android.widget.Filter.FilterResults p1)
	{
		n_publishResults (p0, p1);
	}

	private native void n_publishResults (java.lang.CharSequence p0, android.widget.Filter.FilterResults p1);

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
