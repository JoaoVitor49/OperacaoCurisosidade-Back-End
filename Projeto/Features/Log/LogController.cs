using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Features.Log.Commands;
using Projeto.Features.Log.Queries;

namespace Projeto.Features.Log
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogHandlerManager _handlers;

        public LogController(LogHandlerManager handlers)
        {
            _handlers = handlers;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog(CreateLogCommand command)
        {
            var newLog = await _handlers.Create.Handle(command);
            return CreatedAtAction(nameof(CreateLog), new { id = newLog.Id }, newLog);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLogs([FromQuery] GetAllLogsQuery query)
        {
            var logs = await _handlers.GetAll.Handle(query, User);
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLogById(int id)
        {
            var query = new GetLogByIdQuery { Id = id };
            var log = await _handlers.GetById.Handle(query);
            return Ok(log);
        }
    }
}
