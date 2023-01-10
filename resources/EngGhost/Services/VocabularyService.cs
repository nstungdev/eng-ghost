using EngGhost.FormModels;
using EngGhost.Helpers;
using EngGhost.Infrastructure;
using EngGhost.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngGhost.Services
{
    public class VocabularyService
    {
        private readonly EngGhostDbContext _dbContext;
        public VocabularyService()
        {
            _dbContext = EngGhostDbContext.Instance;
        }

        public async Task<IEnumerable<Vocabulary>> GetVocabulariesAsync()
        {
            var vocabularies = await _dbContext.Vocabularies.GetAllAsync();
            return vocabularies;
        }

        public async Task CreateOneAsync(VocabularyForm form)
        {
            #if DEBUG
            //await Task.Delay(1000);
            #endif

            // normalize form
            form.NormalizeForm();
            var model = Mapper.Map<Vocabulary>(form);
            await _dbContext.Vocabularies.AddAsync(model);
        }
    }
}
