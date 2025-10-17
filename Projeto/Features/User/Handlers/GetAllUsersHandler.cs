using AutoMapper;
using Projeto.Features.User.Dtos;
using Projeto.Features.User.Queries;
using Projeto.Repositories;

namespace Projeto.Features.User.Handlers
{
    public class GetAllUsersHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserResponseDTO>> Handle(GetAllUsersQuery query)
        {
            if (query.Page <= 0) query.Page = 1;
            if (query.Limit <= 0) query.Limit = 10;
            var users = await _userRepository.GetAllUsersAsync(query);
            return _mapper.Map<List<UserResponseDTO>>(users);
        }
    }
}
