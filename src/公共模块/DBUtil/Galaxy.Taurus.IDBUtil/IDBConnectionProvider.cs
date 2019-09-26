using System.Data;

namespace Galaxy.Taurus.IDBUtil
{
    public interface IDBConnectionProvider
    {
        string GetConnectionString();
    }
}
