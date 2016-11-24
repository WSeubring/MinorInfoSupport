using System;
using RawRabbit.vNext.Disposable;
using Export.Responses;
using Common;

namespace Export.Commands
{
    public class TestCommand
    {
        public string Message { get; private set; }

        public TestCommand(string message)
        {
            Message = message;
        }

        internal static void SetupResponseToClient(IBusClient client)
        {
            client.RespondAsync<TestCommand, TestResponse>(async (request, context) =>
            {
                //Eigen action
                Console.WriteLine(request.Message);
                var result = new TestResponse(ResponseStatus.Ok);
                return result;
            });
        }
    }
}