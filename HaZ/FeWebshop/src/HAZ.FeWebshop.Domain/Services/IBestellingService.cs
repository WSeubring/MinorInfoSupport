using HAZ.FeWebshop.Domain.Entities;

namespace HAZ.FeWebshop.Domain.Services
{
    public interface IBestellingService
    {
        BestellingResult PlaatsBestelling(Bestelling bestelling);
    }
}
