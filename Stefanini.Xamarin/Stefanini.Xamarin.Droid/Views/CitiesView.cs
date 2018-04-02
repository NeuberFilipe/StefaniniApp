using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Java.Lang;
using Java.Util;
using MvvmCross.Droid.Support.V7.AppCompat;
using Newtonsoft.Json;
using Stefanini.BackEnd.Core.Model;
using Stefanini.Xamarin.Core.ViewModels;
using Object = System.Object;
using String = System.String;

namespace Stefanini.Xamarin.Droid.Views
{
    [Activity(Label = "Selecione a Cidade")]
    public class CitiesView : MvxAppCompatActivity
    {
        #region Properties

        private SearchView _sv;
        private ListView _lv;
        private ArrayAdapter _newAdapter;
        private ArrayList _weatherArrayString;
        private WeatherViewModel _weather;
        private const string _KeyApi = "2bac87e0cb16557bff7d4ebcbaa89d2f";

        #endregion

        #region Initiailze
        protected int LayoutResource => Resource.Layout.CitiesView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

                #region Initilizar Itens View

                SetContentView(Resource.Layout.CitiesView);
                _lv = FindViewById<ListView>(Resource.Id.lv);
                _sv = FindViewById<SearchView>(Resource.Id.sv);
                _weatherArrayString = new ArrayList();

                #endregion

                await GetJson();

                GetAdapter();

                _lv.Adapter = _newAdapter;

                #region Events

                _lv.ItemClick += _lv_ItemClick;

                _sv.QueryTextChange += _sv_QueryTextChange;

                #endregion
        }
        #endregion

        #region Methods

        private async Task GetJson()
        {
            using (StreamReader sr = new StreamReader(Assets.Open("city.list.json")))
            {
                var json = sr.ReadToEnd();
                var jsonresult = JsonConvert.DeserializeObject<ListClimateViewModel.RootObject>(json);

                new List<ListClimateViewModel.Datum>();
                new List<TempViewModel.RootObject>();
                foreach (var item in jsonresult.data)
                {
                    await ResquestWebApi(item.name);
                }
            }
        }

        private void GetAdapter()
        {
            if (_weatherArrayString != null)
            {
                _newAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,  _weatherArrayString.ToArray());
            }
        }

        public async Task<TempViewModel.RootObject> ResquestWebApi(string name)
        {
            var request = CreateRequest(name);

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream sr = response.GetResponseStream())
                {
                    using (StreamReader ste = new StreamReader(sr))
                    {
                        var json = ste.ReadToEnd();
                        var jsonresult = JsonConvert.DeserializeObject<TempViewModel.RootObject>((string)json);

                        if (jsonresult != null)
                        {
                            SetWeatherList(jsonresult);
                        }

                        return jsonresult;
                    }
                }
            }
        }

        public async Task<TempViewModel.RootObject> ResquestWebApiEvent(string name)
        {
            var request = CreateRequest(name);

            Thread.Sleep(25000);

            using (WebResponse response = await request.GetResponseAsync())
            {
                using (Stream sr = response.GetResponseStream())
                {
                    using (StreamReader ste = new StreamReader(sr))
                    {
                        var json = ste.ReadToEnd();
                        var jsonresult = JsonConvert.DeserializeObject<TempViewModel.RootObject>((string)json);

                        if (jsonresult != null)
                        {
                            SetWeatherListEvent(jsonresult);
                        }

                        return jsonresult;
                    }
                }
            }
        }

        private static HttpWebRequest CreateRequest(string name)
        {
            string urlWebApi = "http://api.openweathermap.org/data/2.5/weather?q=" + name + "&units=metric" +
                               "&appid=" + _KeyApi;

            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(new Uri(urlWebApi));
            request.ContentType = "application/json";
            request.Method = "GET";
            return request;
        }

        private void SetWeatherList(TempViewModel.RootObject jsonresult)
        {
            if (jsonresult != null)
            {
                _weatherArrayString.Add(jsonresult.name);
            }
        }

        private void SetWeatherListEvent(TempViewModel.RootObject jsonresult)
        {
            if (jsonresult != null)
            {
                _weather = new WeatherViewModel()
                {
                    City = jsonresult.name,
                    Weather = jsonresult.weather[0].description,
                    Temp = string.Format("{0} {1}", jsonresult.main.temp, "°C"),
                    TempMax = string.Format("{0} {1}", jsonresult.main.temp_max, "°C"),
                    TempMin = string.Format("{0} {1}", jsonresult.main.temp_min, "°C"),
                    Url = $"http://openweathermap.org/img/w/" + jsonresult.weather[0].icon + ".png"
                };
            }
        }

        private Intent SetIntent(AdapterView.ItemClickEventArgs e)
        {
            Object weatherViewModel = _lv.GetItemAtPosition(e.Position);
            ResquestWebApiEvent(weatherViewModel.ToString());


            Intent setIntent = new Intent(this, typeof(DetailsView));
            if (_weather !=null)
            {
                setIntent.PutExtra("city", _weather.City);
                setIntent.PutExtra("temp", _weather.Temp);
                setIntent.PutExtra("weather", _weather.Weather);
                setIntent.PutExtra("tempmax", _weather.TempMax);
                setIntent.PutExtra("tempmin", _weather.TempMin);
                setIntent.PutExtra("weathericon", _weather.Url);
                return setIntent;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region ImplementetinEvents

        private void _lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, _newAdapter.GetItem(e.Position).ToString(), ToastLength.Short).Show();
            var intent = SetIntent(e);
            if (intent !=null)
            {
                StartActivity(intent);
            }
        }


        void _sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.NewText))
            {
                _newAdapter.Filter.InvokeFilter(e.NewText);
            }
            _lv.Adapter = _newAdapter;
        }

        #endregion

    }
}