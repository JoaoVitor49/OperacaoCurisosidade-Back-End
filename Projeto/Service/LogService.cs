using Projeto.Features.Client.Dtos;
using Projeto.Features.Log.Commands;
using Projeto.Features.Log.Handlers;

namespace Projeto.Service
{
    public class LogService : ILogService
    {
        private readonly CreateLogHandler _createLogHandler;

        public LogService(CreateLogHandler createLogHandler)
        {
            _createLogHandler = createLogHandler;
        }

        public async Task LogUserLoginAsync(string userEmail)
        {
            var command = new CreateLogCommand
            {
                User = userEmail,
                Action = "Usuario fez login",
                OldData = null,
                NewData = null
            };
            await _createLogHandler.Handle(command);
        }

        public async Task LogClientCreatedAsync(string clientEmail, string createdBy)
        {
            var command = new CreateLogCommand
            {
                User = createdBy,
                Action = $"Cadastrou o cliente: {clientEmail}",
                OldData = null,
                NewData = null
            };

            await _createLogHandler.Handle(command);
        }

        public async Task LogClientUpdatedAsync(string clientEmail, ClientAuditDTO oldData, ClientAuditDTO newData, string updatedBy)
        {
            var command = new CreateLogCommand
            {
                User = updatedBy,
                Action = $"Editou o cliente: {clientEmail}",
                OldData = oldData,
                NewData = newData
            };

            await _createLogHandler.Handle(command);
        }

        public async Task LogClientDeletedAsync(string clientEmail, string deletedBy)
        {
            var command = new CreateLogCommand
            {
                User = deletedBy,
                Action = $"Deletou o cliente: {clientEmail}",
                OldData = null,
                NewData = null
            };

            await _createLogHandler.Handle(command);
        }
    }
}