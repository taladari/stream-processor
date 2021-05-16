using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Data
{
    public interface IStatisticsRepository
    {
        Task UpsertEventTypeCount(string eventType);
        Task UpsertWordAppearancesCount(string word);
        Task<IDictionary<string, int>> GetEventTypeCount();
        Task<IDictionary<string, int>> GetWordAppearancesCount();
    }
}
