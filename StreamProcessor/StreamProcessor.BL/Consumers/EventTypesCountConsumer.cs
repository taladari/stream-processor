using StreamProcessor.BL.Data;
using StreamProcessor.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessor.BL.Consumers
{
    public class EventTypesCountConsumer : IConsumer<EventMessage>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public EventTypesCountConsumer(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task OnDataArrived(EventMessage message)
        {
            Console.WriteLine($"Successfully parsed event message: Type: {message.EventType}, Data: {message.Data}");
            await _statisticsRepository.UpsertEventTypeCount(message.EventType);
        }

        public void OnError(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
