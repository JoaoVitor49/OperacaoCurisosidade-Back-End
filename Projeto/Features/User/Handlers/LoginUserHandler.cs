using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.User.Commands;
using Projeto.Features.User.Dtos;
using Projeto.Repositories;
using Projeto.Service;

namespace Projeto.Features.User.Handlers
{
    public class LoginUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public LoginUserHandler(IUserRepository userRepository, IMapper mapper, ILogService logService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task<LoginResultDTO> Handle(LoginUserCommand command)
        {
            var user = await _userRepository.GetUserByEmailAsync(command.Email)
                ?? throw new InvalidCredentialsException();
            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
                throw new InvalidCredentialsException();
            var token = TokenService.GenerateToken(user);
            var userDto = _mapper.Map<UserResponseDTO>(user);
            await _logService.LogUserLoginAsync(user.Email);
            return new LoginResultDTO
            {
                User = userDto,
                Token = token
            };
        }
    }
}
