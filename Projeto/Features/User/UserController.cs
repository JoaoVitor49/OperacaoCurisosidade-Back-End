using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Features.User.Commands;
using Projeto.Features.User.Queries;

namespace Projeto.Features.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserHandlerManager _handlers;
        public UserController(UserHandlerManager handlers)
        {
            _handlers = handlers;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var newUser = await _handlers.Create.Handle(command);
            return CreatedAtAction(nameof(CreateUser), new { id = newUser.Id }, newUser);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            var users = await _handlers.GetAll.Handle(query);
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _handlers.GetById.Handle(query);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command)
        {
            command.Id = id;
            var updatedUser = await _handlers.Update.Handle(command);
            return Ok(updatedUser);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _handlers.Delete.Handle(command);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserCommand command)
        {
            var user = await _handlers.Login.Handle(command);
            return Ok(user);
        }
    }
}
