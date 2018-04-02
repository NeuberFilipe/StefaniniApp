using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Square.Picasso;
using Stefanini.Xamarin.Core.ViewModels;
using Stefanini.Xamarin.Droid.Service;

namespace Stefanini.Xamarin.Droid.Views
{
    [Activity(Label = "Detalhes")]
    public class DetailsView : BaseView
    {

        #region Properties

        private string _city;
        private string _temp;
        private string _weather;
        private string _tempmin;
        private string _tempmax;
        private string _urlWeathericon;
        private ImageButton _btnSave;

        #endregion

        #region Initialize
        protected override int LayoutResource => Resource.Layout.DetailsView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);
            SetContentView(Resource.Layout.DetailsView);
            _btnSave = FindViewById<ImageButton>(Resource.Id.save);

            #region GetParams

            GetParamsValue();

            #endregion


            #region Events

            _btnSave.Click += _btnSave_Click;

            #endregion

            ValidatedParams(_city, _temp, _tempmin, _tempmax, _urlWeathericon);
        }

        #endregion

        #region Events
        private void _btnSave_Click(object sender, EventArgs e)
        {
            #region GetParams

            GetParamsValue();

            WeatherService service = new WeatherService();
            WeatherViewModel weather = new WeatherViewModel()
            {
                City = _city,
                Temp = _temp,
                Weather = _weather,
                TempMin = _tempmin,
                TempMax = _tempmax,
                Url = _urlWeathericon
            };
            if (service.InsertUpdate(weather))
            {
                Toast.MakeText(this, "Salvo com sucesso", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Erro ao salvar", ToastLength.Short).Show();
            }

            #endregion
        }
        #endregion

        #region Methods

        private void GetParamsValue()
        {
            _city = Intent.GetStringExtra("city");
            _temp = Intent.GetStringExtra("temp");
            _weather = Intent.GetStringExtra("weather");
            _tempmin = Intent.GetStringExtra("tempmin");
            _tempmax = Intent.GetStringExtra("tempmax");
            _urlWeathericon = Intent.GetStringExtra("weathericon");
        }

        private void ValidatedParams(string city, string temp, string tempmin, string tempmax, string urlWeathericon)
        {
            if (!String.IsNullOrEmpty(city))
            {
                TextView textCity = FindViewById<TextView>(Resource.Id.city);
                textCity.Text = city;
            }

            if (!String.IsNullOrEmpty(temp))
            {
                TextView textTemp = FindViewById<TextView>(Resource.Id.temp);
                textTemp.Text = temp;
            }

            if (!String.IsNullOrEmpty(tempmin))
            {
                TextView textTempMin = FindViewById<TextView>(Resource.Id.tempmin);
                textTempMin.Text = string.Format("Temperatura Minima: {0}", tempmin);
            }

            if (!String.IsNullOrEmpty(tempmax))
            {
                TextView textTempMax = FindViewById<TextView>(Resource.Id.tempmax);
                textTempMax.Text = string.Format("Temperatura Maxima: {0}", tempmax);
            }

            if (!String.IsNullOrEmpty(urlWeathericon))
            {
                ImageView img = (ImageView)FindViewById(Resource.Id.weathericon);
                var url = Android.Net.Uri.Parse(urlWeathericon);
                Picasso.With(this).Load(url).Into(img);
            }
        }
        #endregion

    }
}