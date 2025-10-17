using Projeto.Features.Log.Handlers;

namespace Projeto.Features.Log
{
    public class LogHandlerManager
    {

        public CreateLogHandler Create { get; }
        public GetAllLogsHandler GetAll { get; }
        public GetLogByIdHandler GetById { get; }

        public LogHandlerManager(
            CreateLogHandler create,
            GetAllLogsHandler getAll,
            GetLogByIdHandler getById)
        {
            Create = create;
            GetAll = getAll;
            GetById = getById;
        }
    }
}
