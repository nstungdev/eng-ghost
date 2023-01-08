using EngGhost.Infrastructure.DbSets;
using EngGhost.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Infrastructure
{
    public class EngGhostDbContext
    {
        private static EngGhostDbContext? _instance = null;

        private readonly DbSet<Vocabulary> _vocabularies;
        
        private EngGhostDbContext()
        {
            _vocabularies = new DbSet<Vocabulary>();
        }

        public DbSet<Vocabulary> Vocabularies => _vocabularies;

        public static EngGhostDbContext Instance => _instance ?? (_instance = new EngGhostDbContext());
    }
}
