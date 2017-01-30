using HAZ.PsWinkelen.Domain.Models;

namespace HAZ.PsWinkelen.Domain.Infrastructure.Agents
{
    public interface IBestellingenBeheerServiceAgent
    {
        object PostBestellingToevoegen(BestellingToevoegenCommand command);
    }
}
