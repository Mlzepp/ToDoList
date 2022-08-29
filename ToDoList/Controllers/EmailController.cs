using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Data.Repositories;

namespace ToDoList.Controllers
{
    public class EmailController : Controller, IHostedService
    {
        private readonly IEmailRepository _emailService;
        private readonly IEmailRepository _emailRepository;
        private Timer _timer;

        public EmailController(IEmailRepository emailService, IEmailRepository emailRepository)
        {
            _emailService = emailService;
            _emailRepository = emailRepository;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SenderEmail, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }

        public async void SenderEmail(object state)
        {
            Console.WriteLine("Is working!!");
            var result = await SendEmail();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public new void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task<bool> SendEmail()
        {
            bool sentEmail = true;
            int emailsSent = 0;

            do
            {
                var respId = await _emailRepository.GetId();
                if (respId != null)
                {
                    var respEmail = await _emailRepository.GetEmail(respId.User);

                    if (respEmail != null)
                    {

                        Console.WriteLine("Send email!!");
                        var result = _emailService.SendEmail(respEmail.Email, respId.Id, respId.Title, respId.Description, respId.DueDate);
                        emailsSent++;
                    }
                }
                else
                {
                    sentEmail = false;
                }
            } while (sentEmail == true);
            
            if (emailsSent > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}


