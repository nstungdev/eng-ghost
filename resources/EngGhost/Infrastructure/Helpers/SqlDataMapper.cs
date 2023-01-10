using EngGhost.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Helpers
{
    public static class SqlDataMapper
    {
        public static T ConvertToObject<T>(this DbDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var t = new T();

            var needSelectColumns = new List<(PropertyInfo, SQLiteColumn)>();

            foreach (var prop in props)
            {
                var propAttrs = prop.GetCustomAttributes(true);
                foreach (var attr in propAttrs)
                {
                    if (attr is SQLiteIgnore ignore && ignore.IgnoreEnum == SQLiteIgnoreEnum.Select)
                        continue;

                    if (attr is SQLiteColumn sqliteColum)
                    {
                        needSelectColumns.Add((prop, sqliteColum));
                        break;
                    }
                }
            }

            for (int i = 0; i < rd.FieldCount; i++)
            {
                string fieldName = rd.GetName(i);
                var prop = needSelectColumns.Where(e => fieldName.Equals(e.Item2.Name, StringComparison.OrdinalIgnoreCase))
                    .Select(e => e.Item1)
                    .FirstOrDefault();

                if (prop != null)
                {
                    var value = rd.IsDBNull(i) ? null : rd.GetValue(i);
                    prop.SetValue(t, value);
                }
            }

            return t;
        }
    }
}
