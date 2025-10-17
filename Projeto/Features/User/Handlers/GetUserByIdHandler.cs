using AutoMapper;
using Projeto.Exceptions;
using Projeto.Features.User.Dtos;
using Projeto.Features.User.Queries;
using Projeto.Repositories;
namespace Projeto.Features.User.Handlers
{
    public class GetUserByIdHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponseDTO> Handle(GetUserByIdQuery query)
        {
            var user = await _userRepository.GetUserByIdAsync(query.Id)
                ?? throw new NotFoundException("User not found.");
            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}
