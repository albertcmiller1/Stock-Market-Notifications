using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailAlert.Business
{
    public class ConsumeScopedServiceHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        public IServiceProvider Services { get; }

        public ConsumeScopedServiceHostedService(IServiceProvider services)
        {
            Services = services;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(15),
                TimeSpan.FromMinutes(60));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //this class is very interesting. It is generating Background tasks with hosted services
            //it allows up to start up another class (SendRecurringEmails) as a second thread on startup
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<SendRecurringEmails>();

                scopedProcessingService.CheckAllStocks();
            }
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
