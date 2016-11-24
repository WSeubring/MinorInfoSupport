using CommandReceivingService.Common;
using Common;

namespace Export.Responses
{
    public class StartGameResponse : IResponse
    {
        public ResponseStatus Status { get; set; }

        public StartGameResponse(ResponseStatus status)
        {
            Status = status;
        }
    }
}