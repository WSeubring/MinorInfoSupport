using System;
using RawRabbit.vNext.Disposable;
using Export.Responses;
using Common;

namespace Export.Commands
{
    public class JoinRoomCommand
    {
        public string Player { get; private set; }

        public JoinRoomCommand(string player)
        {
            Player = player;
        }

        internal static void SetupResponseToClient(IBusClient client)
        {
            client.RespondAsync<JoinRoomCommand, JoinRoomResponse>(async (request, context) =>
            {
                //Eigen action
                Console.WriteLine(request.Player);

                var result = new JoinRoomResponse(ResponseStatus.Ok);
                return result;
            });
        }
    }
}