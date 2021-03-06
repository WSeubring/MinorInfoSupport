// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Lapiwe.GMS.FrontEnd.Agents.RDWAgent.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class KeuringsVerzoekCommand
    {
        /// <summary>
        /// Initializes a new instance of the KeuringsVerzoekCommand class.
        /// </summary>
        public KeuringsVerzoekCommand() { }

        /// <summary>
        /// Initializes a new instance of the KeuringsVerzoekCommand class.
        /// </summary>
        public KeuringsVerzoekCommand(Guid? onderhoudsGuid = default(Guid?), string kenteken = default(string), int? kilometerstand = default(int?), string klantnaam = default(string))
        {
            OnderhoudsGuid = onderhoudsGuid;
            Kenteken = kenteken;
            Kilometerstand = kilometerstand;
            Klantnaam = klantnaam;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "onderhoudsGuid")]
        public Guid? OnderhoudsGuid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "kenteken")]
        public string Kenteken { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "kilometerstand")]
        public int? Kilometerstand { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "klantnaam")]
        public string Klantnaam { get; set; }

    }
}
