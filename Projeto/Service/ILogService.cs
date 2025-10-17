using Projeto.Features.Client.Dtos;

namespace Projeto.Service
{
    public interface ILogService
    {
        Task LogUserLoginAsync(string userEmail);
        Task LogClientCreatedAsync(string clientEmail, string createdBy);
        Task LogClientUpdatedAsync(string clientEmail, ClientAuditDTO oldData, ClientAuditDTO newData, string updatedBy);
        Task LogClientDeletedAsync(string clientEmail, string deletedBy);
    }
}
