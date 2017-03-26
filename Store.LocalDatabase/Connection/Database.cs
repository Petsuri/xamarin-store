using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Store.Interface.Platform;

namespace Store.LocalDatabase.Connection
{
    public class Database<T> : IDatabase<T> where T : class, new()
    {
        private const string DatabaseFileName = "xamarin-store.db";
        private SQLiteConnection m_connection;

        public Database(IFileInformation file)
        {
            m_connection = new SQLiteConnection(file.GetPath(DatabaseFileName));
            m_connection.CreateTable<T>();
        }

        public Task<int> InsertAsync(T item)
        {
            return Task.Run<int>(() =>
            {
                return m_connection.Insert(item, typeof(T));
            });   
        }

        public async Task UpdateAsync(T item)
        {
            await Task.Run(() =>
            {
                m_connection.Update(item, typeof(T));
            });
        }

        public async Task DeleteAsync(int id)
        {
            await Task.Run(() =>
            {
                m_connection.Delete<T>(id);
            });
        }

        public Task<T> LoadAsync(int id)
        {            
            return Task.Run<T>(() =>
            {
                return m_connection.Get<T>(id);   
            });
        }

        public Task<IEnumerable<T>> LoadAllAsync()
        {
            return Task.Run<IEnumerable<T>>(() =>
            {
                return m_connection.Table<T>();
            });
        }
        
    }
}
