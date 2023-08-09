using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Interfaces;
using System.Text.RegularExpressions;

using TestTask;
using Microsoft.AspNetCore.SignalR;
using TestTask.Service;

namespace MetrixTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetrixController : ControllerBase
    {
        private readonly ILogger<MetrixController> _logger;
        private readonly IMetrixService MetrixService;

        public MetrixController(ILogger<MetrixController> logger, IMetrixService metrixService)
        {
            _logger = logger;
            MetrixService = metrixService;
        }

        [HttpPost("send-time-to-clients")]
        public async Task SendTimeToClients(int time)
        {
            await MetrixService.SendTimeToClients(time);

        }
    }
}

