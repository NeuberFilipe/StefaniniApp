using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Newtonsoft.Json;
using Stefanini.BackEnd.Core.Model;
using Stefanini.Xamarin.Core.ViewModels;
using Stefanini.Xamarin.Droid.DataBase;
using Stefanini.Xamarin.Droid.Resources;
using Stefanini.Xamarin.Droid.Service;
using Object = Java.Lang.Object;

namespace Stefanini.Xamarin.Droid.Views
{
    [Activity(Label = "Clima", MainLauncher = true)]
    public class WeatherView : MvxAppCompatActivity
    {

        #region Properties

        private SearchView _sv;
        private ListView _lv;
        private CustomListViewAdapter _newAdapter;
        private List<WeatherViewModel> _weatherList;
        private const string _KeyApi = "2bac87e0cb16557bff7d4ebcbaa89d2f";

        #endregion

        #region Initiailze
        protected int LayoutResource => Resource.Layout.WeatherView;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            #region InitilizeDB
            StefaniniContextAndroid db = new StefaniniContextAndroid();
            db.CreateDataBase();
            #endregion


            WeatherService weatherService = new WeatherService();
            var resultSerivce = weatherService.GetAll();
            if (resultSerivce != null && resultSerivce.Count > 0)
            {
                _weatherList = resultSerivce;
                #region Initilizar Itens View

                SetContentView(Resource.Layout.WeatherView);
                _lv = FindViewById<ListView>(Resource.Id.lv);
                _sv = FindViewById<SearchView>(Resource.Id.sv);
                _weatherList = new List<WeatherViewModel>();

                #endregion

                await GetJson();


                GetAdapter();

                _lv.Adapter = _newAdapter;

                #region Events

                _lv.ItemClick += _lv_ItemClick;

                _sv.QueryTextChange += _sv_QueryTextChange;

                #endregion
            }
            else
            {
                Toast.MakeText(this, "Voçê não possui cidade favorita, favor adicionar uma....", ToastLength.Short).Show();
                Intent setIntent = new Intent(this, typeof(CitiesView));
                StartActivity(setIntent);
            }
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
            if (_weatherList != null)
            {
                _newAdapter = new CustomListViewAdapter(this, _weatherList);
            }
        }

        public async Task<TempViewModel.RootObject> ResquestWebApi(string name)
        {
            string urlWebApi = "http://api.openweathermap.org/data/2.5/weather?q=" + name + "&units=metric" +
                               "&appid=" + _KeyApi;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(urlWebApi));
            request.ContentType = "application/json";
            request.Method = "GET";

            try
            {
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
            catch (Exception ex)
            {
                Log.Warn("ResquestWebApi", "Erro Resquest WebApi OpenWeatherMap");
                return null;
            }
        }

        private void SetWeatherList(TempViewModel.RootObject jsonresult)
        {
            if (jsonresult != null)
            {
                _weatherList.Add(new WeatherViewModel()
                {
                    City = jsonresult.name,
                    Weather = jsonresult.weather[0].description,
                    Temp = string.Format("{0} {1}", jsonresult.main.temp, "°C"),
                    TempMax = string.Format("{0} {1}", jsonresult.main.temp_max, "°C"),
                    TempMin = string.Format("{0} {1}", jsonresult.main.temp_min, "°C"),
                    Url = $"http://openweathermap.org/img/w/" + jsonresult.weather[0].icon + ".png"
                });
            }
        }

        private Intent SetIntent(AdapterView.ItemClickEventArgs e)
        {
            Object weatherViewModel = _lv.GetItemAtPosition(e.Position);
            Intent setIntent = new Intent(this, typeof(DetailsView));

            if (weatherViewModel != null)
            {
                var propertyInfo = weatherViewModel.GetType().GetProperty("Instance");
                var proertyValue = (WeatherViewModel)propertyInfo.GetValue(weatherViewModel, null);
                if (proertyValue != null)
                {
                    setIntent.PutExtra("city", proertyValue.City);
                    setIntent.PutExtra("temp", proertyValue.Temp);
                    setIntent.PutExtra("weather", proertyValue.Weather);
                    setIntent.PutExtra("tempmax", proertyValue.TempMax);
                    setIntent.PutExtra("tempmin", proertyValue.TempMin);
                    setIntent.PutExtra("weathericon", proertyValue.Url);
                }
            }
            return setIntent;
        }

        #endregion

        #region ImplementetinEvents

        private void _lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = SetIntent(e);
            StartActivity(intent);
        }

        void _sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.NewText))
            {
                _newAdapter.Filter.InvokeFilter(e.NewText);

                _sv.QueryTextChange += (s, x) => _newAdapter.Filter.InvokeFilter(e.NewText);

                _sv.QueryTextSubmit += (s, x) =>
                {
                    Toast.MakeText(this, "Searched for: " + x.Query, ToastLength.Short).Show();
                    e.Handled = true;
                };
            }
            else
            {
                GetAdapter();
            }
            _lv.Adapter = _newAdapter;
        }

        #endregion

    }
}