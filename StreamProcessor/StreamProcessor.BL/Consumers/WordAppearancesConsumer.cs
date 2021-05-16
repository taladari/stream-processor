using StreamProcessor.BL.Data;
using StreamProcessor.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Consumers
{
    public class WordAppearancesConsumer : IConsumer<EventMessage>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public WordAppearancesConsumer(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task OnDataArrived(EventMessage message)
        {
            Console.WriteLine($"Successfully parsed event message: Type: {message.EventType}, Data: {message.Data}");
            await _statisticsRepository.UpsertWordAppearancesCount(message.Data);
        }

        public void OnError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
