using CommandReceivingService.Common;
using Common;

namespace Export.Responses
{
    public class JoinRoomResponse : IResponse
    {
        public ResponseStatus Status { get; set; }

        public JoinRoomResponse(ResponseStatus status)
        {
            Status = status;
        }
    }
}