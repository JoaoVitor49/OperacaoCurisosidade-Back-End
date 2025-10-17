using Projeto.Exceptions;
using Projeto.Features.User.Commands;
using Projeto.Repositories;

namespace Projeto.Features.User.Handlers
{
    public class DeleteUserHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand command)
        {
            var deleted = await _userRepository.DeleteUserAsync(command.Id);
            if (!deleted)
                throw new NotFoundException("User not found.");
        }
    }
}
