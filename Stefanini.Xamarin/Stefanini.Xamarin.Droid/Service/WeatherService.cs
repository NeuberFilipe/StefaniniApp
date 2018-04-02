using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SQLite.Net;
using SQLite.Net.Interop;
using Stefanini.BackEnd.Core.Data;
using Stefanini.Xamarin.Core.ViewModels;
using Stefanini.Xamarin.Droid.DataBase;
using Environment = System.Environment;

namespace Stefanini.Xamarin.Droid.Service
{
  public class WeatherService : IStefaniniContext
    {
        public SQLiteConnection connectionSQLite;
        private string _directorySQLite;
        private ISQLitePlatform _platform;
        private StefaniniContextAndroid _db;
        public bool InsertUpdate(WeatherViewModel weather)
        {
            try
            {
                using (connectionSQLite = new SQLiteConnection(Platform, Path.Combine(DirectorySQLite, "StefaniniBD.db3")))
                {
                    if (connectionSQLite.Table<WeatherViewModel>().Any(p => p.City == weather.City))
                    {
                        Log.Info("SQLiteEx", "The informed city has already been added to favorites");
                        return false;
                    }
                    else
                    {
                        connectionSQLite.Insert(weather);
                        Log.Info("SQLiteEx", "City added to favorites");
                        return true;
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool Update(WeatherViewModel weather)
        {
            try
            {
                using (connectionSQLite = new SQLiteConnection(Platform, Path.Combine(DirectorySQLite, "StefaniniBD.db3")))
                {
                    connectionSQLite.Query<WeatherViewModel>("Update WeatherViewModel set  City=? , Weather=? , Temp=?, TempMin=? , TempMax=? , Url=?", 
                                                              weather.City, weather.Weather, weather.Temp, weather.TempMin, weather.TempMax, weather.Url);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public bool Delete(WeatherViewModel weather)
        {
            try
            {
                using (connectionSQLite = new SQLiteConnection(Platform, Path.Combine(DirectorySQLite, "StefaniniBD.db3")))
                {
                    connectionSQLite.Delete(weather);
                    return true;

                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

        public List<WeatherViewModel> GetAll()
        {
            try
            {
                using (connectionSQLite = new SQLiteConnection(Platform, Path.Combine(DirectorySQLite, "StefaniniBD.db3")))
                {
                    return connectionSQLite.Table<WeatherViewModel>().Where(p => p.Favorite).ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return null;
            }
        }


        public string DirectorySQLite
        {
            get
            {
                if (string.IsNullOrEmpty(_directorySQLite))
                {
                    _directorySQLite = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }
                return _directorySQLite;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (_platform == null)
                {
                    _platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                }
                return _platform;
            }
        }
    }
}