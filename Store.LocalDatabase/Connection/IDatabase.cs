using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.LocalDatabase.Connection
{
    public interface IDatabase
    {
        Task DeleteAsync<T>(T item) where T : class, new();
        Task UpdateAsync<T>(T item) where T : class, new();
        Task<int> InsertAsync<T>(T item) where T : class, new();

        Task<T> LoadAsync<T>(int id) where T : class, new();
        Task<IEnumerable<T>> LoadAllAsync<T>() where T : class, new();
        
    }
}