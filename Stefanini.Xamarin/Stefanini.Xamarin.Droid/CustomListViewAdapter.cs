using System.Collections.Generic;
using System.Linq;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Content;
using Stefanini.Xamarin.Core.ViewModels;

namespace Stefanini.Xamarin.Droid.Resources
{
    public class CustomListViewAdapter : BaseAdapter<WeatherViewModel>, IFilterable
    {

        #region Properties
        private List<WeatherViewModel> _mItems;
        private List<WeatherViewModel> _originalData;
        private readonly Context _mContext;
        private View _convertViewNew;

        public override int Count
        {
            get
            {
                if (_mItems != null)
                    return _mItems.Count;
                else
                {
                    return 0;
                }
            }
        }

        public Filter Filter { get; private set; }

        #endregion

        #region Methods
        public CustomListViewAdapter(Context context, List<WeatherViewModel> items)
        {
            _mItems = items;
            _mContext = context;
            Filter = new ChemicalFilter(this);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public View ListViewFind(string name)
        {
            View row = _convertViewNew;
            row = LayoutInflater.From(_mContext).Inflate(Resource.Layout.listviewrow, null, false);
            TextView newName = row.FindViewById<TextView>(Resource.Id.name);

            if (_mItems[0].City.Contains(name))
            {
                newName.Text = _mItems[0].City;
            }
            else
            {
                _mItems[0].City = string.Empty;
            }
            return row;
        }

        public override WeatherViewModel this[int position]
        {
            get { return _mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup Parent)
        {
            View row = convertView;
            _convertViewNew = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(_mContext).Inflate(Resource.Layout.listviewrow, null, false);
            }

            TextView name = row.FindViewById<TextView>(Resource.Id.name);
            name.Text = _mItems[position].City;


            TextView temp = row.FindViewById<TextView>(Resource.Id.temp);
            temp.Text = _mItems[position].Temp.ToString();

            TextView weather = row.FindViewById<TextView>(Resource.Id.weather);
            weather.Text = _mItems[position].Weather;
            return row;
        }


        private class ChemicalFilter : Filter
        {
            private readonly CustomListViewAdapter _adapter;

            public ChemicalFilter(CustomListViewAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                _adapter._originalData = new List<WeatherViewModel>();
                var results = new List<WeatherViewModel>();
                if (_adapter == null)
                {
                    _adapter._originalData = _adapter._mItems;
                }

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    results.AddRange(
                        _adapter._originalData.Where(
                            chemical => chemical.City.ToLower().Contains(constraint.ToString())));
                }

                returnObj.Values = FromArray(results.Select(r => r.City.Contains(constraint.ToString())).ToArray());
                returnObj.Count = results.Count;
                constraint.Dispose();
                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter._mItems = values.ToArray<WeatherViewModel>().ToList();
                _adapter.NotifyDataSetChanged();
                constraint.Dispose();
                results.Dispose();
            }
        }

        #endregion

    }
}