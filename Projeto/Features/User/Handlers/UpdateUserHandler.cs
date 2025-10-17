using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.User.Commands;
using Projeto.Features.User.Dtos;
using Projeto.Repositories;

namespace Projeto.Features.User.Handlers
{
    public class UpdateUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(UpdateUserCommand command)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id)
                    ?? throw new NotFoundException("User not found.");

            if (Utils.Utils.ValidateUser(command).Count > 0)
                throw new ValidationException("Validation failed: " 
                    + string.Join(", ", Utils.Utils.ValidateUser(command)));

            var existingUser = await _userRepository.GetUserByEmailAsync(command.Email);
            if (existingUser != null && existingUser.Id != command.Id)
                throw new EmailAlreadyExistsException();

            user.Name = command.Name;
            user.Email = command.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(command.Password);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}