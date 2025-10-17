namespace Projeto.Features.User.Dtos
{
    public class LoginResultDTO
    {
        public UserResponseDTO User{ get; set; }
        public object Token { get; set; }
    }
}
