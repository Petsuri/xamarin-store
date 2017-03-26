using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.LocalDatabase.Connection
{
    public interface IDatabase<T> where T : class, new()
    {
        Task<int> InsertAsync(T item);
        Task DeleteAsync(int id);

        Task<T> LoadAsync(int id);
        Task<IEnumerable<T>> LoadAllAsync();

        Task UpdateAsync(T item);
        
    }
}