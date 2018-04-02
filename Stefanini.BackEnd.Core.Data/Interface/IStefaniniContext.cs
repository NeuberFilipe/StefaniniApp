using SQLite.Net.Interop;

namespace Stefanini.BackEnd.Core.Data
{
    public interface IStefaniniContext
    {
        string DirectorySQLite { get; }
        ISQLitePlatform Platform { get; }
    }
}
