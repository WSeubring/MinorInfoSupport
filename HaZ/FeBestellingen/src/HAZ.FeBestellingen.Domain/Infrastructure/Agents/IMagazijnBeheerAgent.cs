using Kantilever.Magazijnbeheer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HAZ.FeBestellingen.Domain.Infrastructure.Agents
{
    public interface IMagazijnBeheerAgent
    {
        void SendHaalArtikelUitMagazijnCommand(HaalArtikelUitMagazijnCommand command);
    }
}
