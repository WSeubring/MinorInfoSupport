using Common;
using Export.Commands;
using Export.Responses;
using Minor.RPCTestProject.Common;
using Minor.WSA.WSAEventbus;
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
            //RPC using RAWRabbit
            var client = BusClientFactory.CreateDefault();

            //Init Responses
            TestCommand.SetupResponseToClient(client);
            JoinRoomCommand.SetupResponseToClient(client);
            StartGameCommand.SetupResponseToClient(client);

            ////Eventlisten using Minor.WSA.Eventbus 0.1.0
            //using (var bus = new Eventbus())
            //{
            //    bus.Subscribe(new HyperModerneEventHandler());

            //    //Keep service active
            //    while (true)
            //    {
            //        Thread.Sleep(100);
            //    }
            //}

        }
    }
}
