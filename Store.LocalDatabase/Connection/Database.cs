using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Store.Interface.Platform;

namespace Store.LocalDatabase.Connection
{
    public class Database : IDatabase
    {
        private const string DatabaseFileName = "xamarin-store.db";
        private SQLiteAsyncConnection m_connection;

        public Database(IFileInformation file)
        {
            m_connection = new SQLiteAsyncConnection(file.GetPath(DatabaseFileName));
            
        }

        public async Task<int> InsertAsync<T>(T item) where T : class, new()
        {
            return await m_connection.InsertAsync(item);
        }

        public async Task UpdateAsync<T>(T item) where T : class, new()
        {
            await  m_connection.UpdateAsync(item);
        }

        public async Task DeleteAsync<T>(T item) where T : class, new()
        {
            await m_connection.DeleteAsync(item);
        }

        public async Task<T> LoadAsync<T>(int id) where T : class, new()
        {            
            return await m_connection.GetAsync<T>(id);   
        }
    
        public async Task<IEnumerable<T>> LoadAllAsync<T>() where T : class, new()
        {
            IEnumerable<T> loadedItems = await m_connection.Table<T>().ToListAsync();
            return loadedItems;
        }
        
    }
}
