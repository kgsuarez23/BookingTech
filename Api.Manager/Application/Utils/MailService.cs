using Api.Manager.Domain.Entity;
using Microsoft.Extensions.Configuration;
using RazorLight;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Api.Manager.Application.Utils
{
    public class MailService : IMailService
    {
        private readonly RazorLightEngine _razorLightEngine;
        private readonly IConfiguration _config;
        public MailService(IConfiguration config)
        {
            _config = config;

            var templatesPath = Path.Combine(AppContext.BaseDirectory, "Templates");
            _razorLightEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(templatesPath)
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<bool> SendMail(string mail_to, string subject, BookingEntity message)
        {
            try
            {
                string result = await _razorLightEngine.CompileRenderAsync("ReservationEmailTemplate.cshtml", message);

                var correo = new MailMessage()
                {
                    From = new MailAddress(_config["ServiceMail:Mail"], _config["ServiceMail:Alias"]),
                    Subject = subject,
                    Body = result,
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                };

                correo.To.Add(new MailAddress(mail_to));

                var credenciales = new NetworkCredential(_config["ServiceMail:Mail"], _config["ServiceMail:Password"]);

                var clienteServidor = new SmtpClient()
                {
                    Host = _config["ServiceMail:Host"],
                    Port = int.Parse(_config["ServiceMail:Port"]),
                    Credentials = credenciales,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                };

                clienteServidor.Send(correo);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
