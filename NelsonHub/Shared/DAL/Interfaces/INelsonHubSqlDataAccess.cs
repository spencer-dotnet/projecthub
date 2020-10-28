using System.Collections.Generic;
using System.Threading.Tasks;

namespace NelsonHub.Shared.DAL
{
    public interface INelsonHubSqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters);
        Task<T> LoadOneData<T, U>(string sql, U parameters);
        Task<int> SaveData<T>(string sql, T parameters);
    }
}