using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Helpers
{
    public static class Normalizer
    {
        public static void NormalizeForm<T>(this T form) where T : class
        {
            var type = form.GetType();
            var props = type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(e => e.PropertyType == typeof(string));
            foreach (var p in props)
            {
                string? value = p.GetValue(form)?.ToString();
                value = value?.Trim();
                value = string.IsNullOrEmpty(value) ? null : value;
                p.SetValue(form, value);
            }
        }
    }
}
