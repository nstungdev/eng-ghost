using EngGhost.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Helpers
{
    public static class CommandFactory
    {
        #region Helper
        private static string GetTableName(Type type)
        {
            var attrs = Attribute.GetCustomAttributes(type, false);
            var tableName = type.Name;
            var tableNameAtrr = attrs.Where(e => e is SQLiteTableAttribute).FirstOrDefault();
            if (tableNameAtrr != null)
                tableName = ((SQLiteTableAttribute)tableNameAtrr).Name;
            return tableName;
        }
        #endregion

        #region Main Method
        public static void TransformToInsertCommand<T>(this SQLiteCommand command, T entity) where T : class
        {
            var entityType = entity.GetType();

            var tableName = GetTableName(entityType);

            var props = entityType.GetProperties();
            var needInsertColumns = new List<(PropertyInfo, SQLiteColumn)>();

            foreach (var prop in props)
            {
                var propAttrs = prop.GetCustomAttributes(true);
                foreach (var attr in propAttrs)
                {
                    if (attr is SQLiteIgnore ignore && ignore.IgnoreEnum == SQLiteIgnoreEnum.Insert)
                        continue;
                    
                    if (attr is SQLiteColumn sqliteColum)
                    {
                        needInsertColumns.Add((prop, sqliteColum));
                        break;
                    }
                }
            }

            string insertedColumnNames = string.Join(',', needInsertColumns.Select(e => e.Item2.Name));
            string insertedParams = string.Join(',', needInsertColumns.Select(e => $"@_{e.Item1.Name}"));

            string cmdText = $@"INSERT INTO {tableName} ({insertedColumnNames}) VALUES ({insertedParams})";

            // inject parameters
            command.CommandText = cmdText;

            foreach (var c in needInsertColumns)
            {
                var paramName = $"@_{c.Item1.Name}";
                command.Parameters.AddWithValue(paramName, c.Item1.GetValue(entity));
            }
        }
        #endregion
    }
}
