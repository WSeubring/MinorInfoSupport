// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HAZ.PsWinkelen.Domain.Models
{
    using Newtonsoft.Json;

    public partial class OkResult
    {
        /// <summary>
        /// Initializes a new instance of the OkResult class.
        /// </summary>
        public OkResult() { }

        /// <summary>
        /// Initializes a new instance of the OkResult class.
        /// </summary>
        public OkResult(int? statusCode = default(int?))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statusCode")]
        public int? StatusCode { get; private set; }

    }
}
