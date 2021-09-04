using System.Data;

namespace Persistence.DBConnectionFactory
{
    public interface IConnectionFactory
    {
        IDbConnection GetDbConnection();
    }
}