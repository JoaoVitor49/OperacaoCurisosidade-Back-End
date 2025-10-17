using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Features.Client.Commands;
using Projeto.Features.Client.Queries;
using System.Runtime.InteropServices;

namespace Projeto.Features.Client
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientHandlerManager _handlers;
        public ClientController(ClientHandlerManager handlers)
        {
            _handlers = handlers;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(CreateClientCommand command)
        { 
            var newClient = await _handlers.Create.Handle(command, User);
            return CreatedAtAction(nameof(CreateClient), new { id = newClient.Id }, newClient);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients([FromQuery] GetAllClientQuery query)
        {
            var clients = await _handlers.GetAll.Handle(query, User);
            return Ok(clients);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var query = new GetClientByIdQuery { Id = id }; 
            var client = await _handlers.GetById.Handle(query);
            return Ok(client);
        }

        [HttpGet("stats/total")]
        public async Task<IActionResult> GetTotalClients([FromQuery] GetTotalClientQuery query)
        {
            var total = await _handlers.GetTotal.Handle(query, User);
            return Ok(total);
        }

        [HttpGet("stats/lastMonth")]
        public async Task<IActionResult> GetLastMonthClients([FromQuery] GetLastMonthClientQuery query)
        {
            var total = await _handlers.GetLastMonth.Handle(query, User);
            return Ok(total);
        }

        [HttpGet("stats/inactive")]
        public async Task<IActionResult> GetInactiveClients([FromQuery] GetInactiveClientQuery query)
        {
            var total = await _handlers.GetInactive.Handle(query, User);
            return Ok(total);
        }

        [HttpGet("print")]
        public async Task<IActionResult> PrintClients()
        {
            var clients = await _handlers.Print.Handle(User);
            return Ok(clients);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, UpdateClientCommand command)
        {
            command.Id = id;
            var updatedClient = await _handlers.Update.Handle(command, User);
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var command = new DeleteClientCommand { Id = id };
            await _handlers.Delete.Handle(command, User);
            return NoContent();
        }
    }
}