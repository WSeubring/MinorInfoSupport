// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace HAZ.PsWinkelen.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class BestellingToevoegenCommand
    {
        /// <summary>
        /// Initializes a new instance of the BestellingToevoegenCommand class.
        /// </summary>
        public BestellingToevoegenCommand() { }

        /// <summary>
        /// Initializes a new instance of the BestellingToevoegenCommand class.
        /// </summary>
        public BestellingToevoegenCommand(DateTime? datumBestelling = default(DateTime?), double? totaalBedragInc = default(double?), double? totaalBedragExc = default(double?), IList<Bestelregel> bestelregels = default(IList<Bestelregel>), Klantgegevens klantgegevens = default(Klantgegevens), string bestelStatus = default(string))
        {
            DatumBestelling = datumBestelling;
            TotaalBedragInc = totaalBedragInc;
            TotaalBedragExc = totaalBedragExc;
            Bestelregels = bestelregels;
            Klantgegevens = klantgegevens;
            BestelStatus = bestelStatus;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "datumBestelling")]
        public DateTime? DatumBestelling { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totaalBedragInc")]
        public double? TotaalBedragInc { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "totaalBedragExc")]
        public double? TotaalBedragExc { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bestelregels")]
        public IList<Bestelregel> Bestelregels { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "klantgegevens")]
        public Klantgegevens Klantgegevens { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bestelStatus")]
        public string BestelStatus { get; set; }

    }
}
