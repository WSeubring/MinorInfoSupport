using Lapiwe.KlantBeheerService.Domain;

namespace Lapiwe.KlantBeheerService.Infrastructure
{
    public interface IRepository
    {
        void Insert(Klant klant);
    }
}

