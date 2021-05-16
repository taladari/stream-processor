using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Data
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly EventsDbContext _dbContext;

        public StatisticsRepository(EventsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDictionary<string, int>> GetEventTypeCount() =>
            await _dbContext.EventTypesCount.ToDictionaryAsync(x => x.EventType, x => x.Count);

        public async Task<IDictionary<string, int>> GetWordAppearancesCount() =>
            await _dbContext.WordAppearancesCount.ToDictionaryAsync(x => x.Word, x => x.Count);

        public async Task UpsertEventTypeCount(string eventType)
        {
            var type = await _dbContext.EventTypesCount.FirstOrDefaultAsync(x => x.EventType == eventType);
            if (type is not null)
                type.Count++;
            else
            {
                type = new EventTypeCount { EventType = eventType, Count = 1 };
                _dbContext.EventTypesCount.Add(type);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpsertWordAppearancesCount(string word)
        {
            var type = await _dbContext.WordAppearancesCount.FirstOrDefaultAsync(x => x.Word == word);
            if (type is not null)
                type.Count++;
            else
            {
                type = new WordAppearancesCount { Word = word, Count = 1 };
                _dbContext.WordAppearancesCount.Add(type);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
