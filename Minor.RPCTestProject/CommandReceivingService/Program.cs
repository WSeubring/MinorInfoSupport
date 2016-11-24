using Common;
using Export.Commands;
using Export.Responses;
using RawRabbit.vNext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandReceivingService
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var client = BusClientFactory.CreateDefault();

            //Init Responses
            TestCommand.SetupResponseToClient(client);
            JoinRoomCommand.SetupResponseToClient(client);
            StartGameCommand.SetupResponseToClient(client);

            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
