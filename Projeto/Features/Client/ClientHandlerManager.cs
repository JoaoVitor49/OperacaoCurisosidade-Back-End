using Projeto.Features.Client.Handlers;

namespace Projeto.Features.Client
{
    public class ClientHandlerManager
    {
        public CreateClientHandler Create { get; }
        public UpdateClientHandler Update { get; }
        public DeleteClientHandler Delete { get; }
        public GetAllClientsHandler GetAll { get; }
        public GetClientByIdHandler GetById { get; }
        public GetTotalClientHandler GetTotal { get; }
        public GetLastMonthClientHandler GetLastMonth { get; }
        public GetInactiveClientHandler GetInactive { get; }
        public PrintClientHandler Print { get; }

        public ClientHandlerManager(
            CreateClientHandler create,
            UpdateClientHandler update,
            DeleteClientHandler delete,
            GetAllClientsHandler getAll,
            GetClientByIdHandler getById,
            GetTotalClientHandler getTotal,
            GetLastMonthClientHandler getLastMonth,
            GetInactiveClientHandler getInactive,
            PrintClientHandler print)
            {
                Create = create;
                Update = update;
                Delete = delete;
                GetAll = getAll;
                GetById = getById;
                GetTotal = getTotal;
                GetLastMonth = getLastMonth;
                GetInactive = getInactive;
                Print = print;
        }

        }
}
