// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lapiwe.GMS.FrontEnd.Agents.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class BadRequestResult
    {
        /// <summary>
        /// Initializes a new instance of the BadRequestResult class.
        /// </summary>
        public BadRequestResult() { }

        /// <summary>
        /// Initializes a new instance of the BadRequestResult class.
        /// </summary>
        public BadRequestResult(int? statusCode = default(int?))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statusCode")]
        public int? StatusCode { get; private set; }

    }
}
