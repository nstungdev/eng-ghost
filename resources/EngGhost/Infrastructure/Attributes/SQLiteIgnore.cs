using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SQLiteIgnore : Attribute
    {
        private readonly SQLiteIgnoreEnum _ignoreEnum;
        public SQLiteIgnore(SQLiteIgnoreEnum ignoreEnum)
        {
            _ignoreEnum = ignoreEnum;
        }
        public SQLiteIgnoreEnum IgnoreEnum => _ignoreEnum;
    }

    public enum SQLiteIgnoreEnum
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
    }
}
