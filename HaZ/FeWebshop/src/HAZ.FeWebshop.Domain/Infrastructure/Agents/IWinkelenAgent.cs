using HAZ.FeWebshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeWebshop.Domain.Infrastructure.Agents
{
    public interface IWinkelenAgent
    {

        BestellingResult PlaatsBestelling(FullBestelling bestelling);

    }
}
