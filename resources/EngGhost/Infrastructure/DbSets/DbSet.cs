using EngGhost.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.DbSets
{
    public class DbSet<T> where T : class, new()
    {
        private readonly SQLiteConnection _conn;
        public DbSet()
        {
            var connectionString = ConfigurationManager.AppSettings.Get("ConnectionString") ?? string.Empty;
            _conn = new SQLiteConnection(connectionString);
        }
        public async Task<int> AddAsync(T entity)
        {
            await OpenConnectionAsync();
            using var cmd = _conn.CreateCommand();
            cmd.ToInsertCommand(entity);
            int rows = await cmd.ExecuteNonQueryAsync();
            return rows;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            await OpenConnectionAsync();
            using var cmd = _conn.CreateCommand();
            cmd.ToSelectCommand<T>();
            using var rd = await cmd.ExecuteReaderAsync();
            var results = new List<T>();
            while (await rd.ReadAsync())
            {
                results.Add(rd.ConvertToObject<T>());
            }
            return results;
        }

        protected async Task OpenConnectionAsync()
        {
            if (_conn == null)
                throw new NullReferenceException("Database connection was null");

            if (_conn.State == System.Data.ConnectionState.Closed)
                await _conn.OpenAsync();
        }
    }
}
