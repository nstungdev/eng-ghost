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
    public class DbSet<T> where T : class
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
            cmd.TransformToInsertCommand(entity);
            int rows = await cmd.ExecuteNonQueryAsync();
            return rows;
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
