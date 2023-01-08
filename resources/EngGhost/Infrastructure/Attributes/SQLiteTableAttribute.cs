using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SQLiteTableAttribute : Attribute
    {
        private string _name;
        public SQLiteTableAttribute(string name)
        {
            _name = name; 
        }
        public string Name => _name;
    }
}
