using Microsoft.Extensions.Hosting;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using back.Protos;
// using


namespace back.Services
{
    public class ClientScheduledService: IHostedService
    {
        private System.Timers.Timer timer;
        private Channel channel;
        private SpringGrpcService.SpringGrpcServiceClient client;

        public ClientScheduledService() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            channel = new Channel("127.0.0.1:8787", ChannelCredentials.Insecure);
            client = new SpringGrpcService.SpringGrpcServiceClient(channel);
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(GetAllAppointments);
            timer.Interval = 3000;
            timer.Enabled = true;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            channel?.ShutdownAsync();
            timer?.Dispose();
            return Task.CompletedTask;
        }

        private async void GetAllAppointments(object source, ElapsedEventArgs e)
        {
            try
            {
                back.Protos.AppointmentListResponseProto response = await client.getAllAppointmentsAsync(new FacilityIdRequest { CenterId = 1});
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
