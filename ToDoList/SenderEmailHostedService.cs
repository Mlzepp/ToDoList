using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Data.Repositories;
using ToDoList.Model;

namespace ToDoList
{
    public class SenderEmailHostedService : IHostedService, IDisposable
    {
        private Timer _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SenderEmail, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;  
        }

        public void SenderEmail(object state)
        {
            Console.WriteLine("Esta funcionando!!");
            //var result = SendEmail();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();  
        }
    }
}
