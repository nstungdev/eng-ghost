using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Helpers
{
    public class Mapper
    {
        public static TDest Map<TDest>(object src)
        {
            var srcProps = src.GetType().GetProperties();
            var destProps = typeof(TDest).GetProperties();

            var destObj = Activator.CreateInstance<TDest>();

            foreach (var prop in destProps)
            {
                var propInfo = srcProps.Where(e => ComparePropName(e.Name, prop.Name)).FirstOrDefault();
                if (propInfo != null)
                {
                    prop.SetValue(destObj, propInfo.GetValue(src));
                }
            }

            return destObj;
        }

        private static bool ComparePropName(string p1, string p2)
        {
            return p1.Equals(p2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
