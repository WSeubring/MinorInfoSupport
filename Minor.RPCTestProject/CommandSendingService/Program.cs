using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RawRabbit;
using RabbitMQ;
using RawRabbit.vNext;
using Export.Responses;
using Export.Commands;
using System.Threading;

namespace CommandSendingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                var client = BusClientFactory.CreateDefault();
                Guid guid = new Guid();
                JoinRoomResponse test = client.RequestAsync<StartGameCommand, JoinRoomResponse>(new StartGameCommand("Test123"), guid).Result;
                Console.WriteLine($"Nu krijg ik Response {test.Status.ToString()}");
                Thread.Sleep(500);
            }
        }
    }
}
