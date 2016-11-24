using System;
using RawRabbit.vNext.Disposable;
using Export.Responses;
using Common;

namespace Export.Commands
{
    public class StartGameCommand
    {
        public string Message { get; private set; }

        public StartGameCommand(string message)
        {
            Message = message;
        }

        internal static void SetupResponseToClient(IBusClient client)
        {
            client.RespondAsync<StartGameCommand, StartGameResponse>(async (request, context) =>
            {
                //Eigen action

                var result = new StartGameResponse(ResponseStatus.Ok);
                return result;
            });
        }
    }
}