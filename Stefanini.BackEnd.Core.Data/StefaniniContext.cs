using System;
using System.IO;
using SQLite.Net;
using Stefanini.BackEnd.Core.Model;
using Xamarin.Forms;

namespace Stefanini.BackEnd.Core.Data
{
    public class StefaniniContext : IDisposable
    {
        public SQLiteConnection connectionSQLite;

        public StefaniniContext()
        {
            var config = DependencyService.Get<IStefaniniContext>();
            connectionSQLite = new SQLiteConnection(config.Platform, Path.Combine(config.DirectorySQLite, "StefaniniBD.db3"));
            connectionSQLite.CreateTable<ListClimateViewModel.Datum>();
            connectionSQLite.CreateTable<ListClimateViewModel.Coord>();
        }

        public void Dispose()
        {
            connectionSQLite.Dispose();
        }
    }
}
