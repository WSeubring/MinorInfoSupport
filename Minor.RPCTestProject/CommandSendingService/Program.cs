using Minor.WSA.WSAEventbus;
using Minor.RPCTestProject.Common;

namespace CommandSendingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////RPC Send a Command
            //var client = BusClientFactory.CreateDefault();
            //Guid guid = new Guid();
            //JoinRoomResponse test = client.RequestAsync<StartGameCommand, JoinRoomResponse>(new StartGameCommand("Test123"), guid).Result;
            //Console.WriteLine($"Nu krijg ik Response {test.Status.ToString()}");
            //Thread.Sleep(500);

            //Publish a event using Minor.WSA.Eventbus 0.1.0
            using (var bus = new Eventbus())
            {
                bus.PublishEvent(new HypermodernEvent() { HypermodernGetal = 10 });
            }
        }
    }
}
