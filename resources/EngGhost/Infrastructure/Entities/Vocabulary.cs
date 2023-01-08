using EngGhost.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure.Entities
{
    [SQLiteTable("vocabulary")]
    public class Vocabulary
    {
        [SQLiteIgnore(SQLiteIgnoreEnum.Insert)]
        public int Id { get; set; }

        [SQLiteColumn("word")]
        public string? Word { get; set; }

        [SQLiteColumn("wordType")]
        public string? WordType { get; set; }

        [SQLiteColumn("mean")]
        public string? Mean { get; set; }

        [SQLiteColumn("pronounce")]
        public string? Pronounce { get; set; }

        [SQLiteColumn("note")]
        public string? Note { get; set; }
    }
}
