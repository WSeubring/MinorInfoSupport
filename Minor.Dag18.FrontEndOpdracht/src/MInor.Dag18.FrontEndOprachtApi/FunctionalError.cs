namespace MInor.Dag18.FrontEndOprachtApi.Controllers
{
    internal class FunctionalError
    {
        public string ErrorCode{ get; set; }
        public string ErrorMessage { get; set; }

        public FunctionalError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}