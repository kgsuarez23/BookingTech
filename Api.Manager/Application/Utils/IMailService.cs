using Api.Manager.Domain.Entity;

namespace Api.Manager.Application.Utils
{
    public interface IMailService
    {
        Task<bool> SendMail(string mail_to, string subject, BookingEntity message);
    }
}
