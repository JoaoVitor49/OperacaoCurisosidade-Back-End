using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.User.Commands;
using Projeto.Features.User.Dtos;
using Projeto.Repositories;

namespace Projeto.Features.User.Handlers
{
    public class CreateUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(CreateUserCommand command)
        {
            if(Utils.Utils.ValidateUser(command).Count > 0)
                throw new ValidationException("Validation failed: " 
                    + string.Join(", ", Utils.Utils.ValidateUser(command)));

            var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
            if(existingUser != null)
                throw new EmailAlreadyExistsException();

            var newUser = new User()
            {
                Name = command.Name,
                Email = command.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Password)
            };

            var createdUser = await _userRepository.AddUserAsync(newUser);
            return _mapper.Map<UserResponseDTO>(createdUser);
        }
    }
}
