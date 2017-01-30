// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HAZ.PsWinkelen.Infrastructure.Agents
{
    using Newtonsoft.Json;

    public partial class ErrorMessage
    {
        /// <summary>
        /// Initializes a new instance of the ErrorMessage class.
        /// </summary>
        public ErrorMessage() { }

        /// <summary>
        /// Initializes a new instance of the ErrorMessage class.
        /// </summary>
        public ErrorMessage(int? foutType = default(int?), string foutMelding = default(string), string oplossing = default(string))
        {
            FoutType = foutType;
            FoutMelding = foutMelding;
            Oplossing = oplossing;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "foutType")]
        public int? FoutType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "foutMelding")]
        public string FoutMelding { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "oplossing")]
        public string Oplossing { get; set; }

    }
}