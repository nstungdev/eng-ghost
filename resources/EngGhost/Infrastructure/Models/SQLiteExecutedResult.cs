using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Models
{
    public class SQLiteExecutedResult
    {
        public bool IsSuccess { get; set; }
        public int AffectedRows { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
