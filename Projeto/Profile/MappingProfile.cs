using Projeto.Features.Client;
using Projeto.Features.Client.Dtos;
using Projeto.Features.Log;
using Projeto.Features.Log.Dtos;
using Projeto.Features.User;
using Projeto.Features.User.Dtos;

namespace Projeto.Profile
{
    public class MappingProfile  : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>();
            CreateMap<Client, ClientResponseDTO>();
            CreateMap<Client, ClientAuditDTO>();
            CreateMap<Log, LogResponseDTO>();
        }
    }
}