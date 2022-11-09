using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        #region Fields

        private readonly SignalRService _signalRService;

        #endregion

        #region Constructors

        public WorkController(SignalRService signalRService)
        {
            _signalRService = signalRService;
        }

        #endregion

        #region Methods


        [HttpGet("{id}")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(long id)
        {
            var workItem = new WorkItem
            {
                Id = id
            };

            return workItem;
        }

        [HttpPost]
        public async Task<ActionResult<WorkItem>> PostWorkItem(WorkItem workItem)
        {
            var tasks = new[]
            {
                Task.Run(() => PerformWorkAndUpdate())
            };

            return CreatedAtAction(nameof(GetWorkItem), new { id = workItem.Id }, workItem);
        }

        private async Task PerformWorkAndUpdate()
        {
            for (int progress = 1; progress <= 4; progress++)
            {
                await Task.Delay(2000);
                await _signalRService.SendWorkUpdateMessage(25 * progress);
            }
        }

        #endregion
    }
}
