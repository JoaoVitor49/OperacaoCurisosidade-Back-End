using Projeto.Features.User.Handlers;

namespace Projeto.Features.User
{
    public class UserHandlerManager
    {
        public CreateUserHandler Create { get; }
        public UpdateUserHandler Update { get; }
        public DeleteUserHandler Delete { get; }
        public GetAllUsersHandler GetAll { get; }
        public GetUserByIdHandler GetById { get; }
        public LoginUserHandler Login { get; }

        public UserHandlerManager(
            CreateUserHandler create, 
            UpdateUserHandler update, 
            DeleteUserHandler delete, 
            GetAllUsersHandler getAll, 
            GetUserByIdHandler getById,
            LoginUserHandler login)
        {
            Create = create;
            Update = update;
            Delete = delete;
            GetAll = getAll;
            GetById = getById;
            Login = login;
        }
    }
}
