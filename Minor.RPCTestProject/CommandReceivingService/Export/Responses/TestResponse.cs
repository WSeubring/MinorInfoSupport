using CommandReceivingService.Common;
using Common;

namespace Export.Responses
{
    public class TestResponse : IResponse
    {
        public ResponseStatus Status { get; set; }

        public TestResponse(ResponseStatus status)
        {
            Status = status;
        }
    }
}