using System.IO;
using Android.Util;
using SQLite.Net;
using SQLite.Net.Interop;
using Stefanini.BackEnd.Core.Data;
using Stefanini.Xamarin.Core.ViewModels;
using Stefanini.Xamarin.Droid.DataBase;
using Environment = System.Environment;

[assembly: Xamarin.Forms.Dependency(typeof(StefaniniContextAndroid))]

namespace Stefanini.Xamarin.Droid.DataBase
{
    public class StefaniniContextAndroid : IStefaniniContext
    {
        private string _directorySQLite;
        private ISQLitePlatform _platform;
        public SQLiteConnection connectionSQLite;

        public StefaniniContextAndroid()
        {

        }

        public string DirectorySQLite
        {
            get
            {
                if (string.IsNullOrEmpty(_directorySQLite))
                {
                    _directorySQLite = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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

        public bool CreateDataBase()
        {
            try
            {
                using (connectionSQLite = new SQLiteConnection(Platform, Path.Combine(DirectorySQLite, "StefaniniBD.db3")))
                {
                    connectionSQLite.CreateTable<WeatherViewModel>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
    }
}