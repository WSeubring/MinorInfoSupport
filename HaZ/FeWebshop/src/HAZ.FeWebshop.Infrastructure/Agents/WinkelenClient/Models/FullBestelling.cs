// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HAZ.FeWEbshop.Infrastructure.Agents.PsWinkelen.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class FullBestelling
    {
        /// <summary>
        /// Initializes a new instance of the FullBestelling class.
        /// </summary>
        public FullBestelling() { }

        /// <summary>
        /// Initializes a new instance of the FullBestelling class.
        /// </summary>
        public FullBestelling(IList<Artikel> artikelen = default(IList<Artikel>), Klant klant = default(Klant))
        {
            Artikelen = artikelen;
            Klant = klant;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "artikelen")]
        public IList<Artikel> Artikelen { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "klant")]
        public Klant Klant { get; set; }

    }
}
