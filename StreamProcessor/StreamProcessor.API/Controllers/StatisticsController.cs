using Microsoft.AspNetCore.Mvc;
using StreamProcessor.BL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamProcessor.API.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet("event-types")]
        public async Task<IActionResult> GetEventTypes()
        {
            return Ok(await _statisticsRepository.GetEventTypeCount());
        }

        [HttpGet("words")]
        public async Task<IActionResult> GetWordsAppearancesCount()
        {
            return Ok(await _statisticsRepository.GetWordAppearancesCount());
        }
    }
}
