using Minor.Dag18.FrontEndOpdracht.Models;
using System.Collections.Generic;

namespace Agents
{
    public interface IMonumentenAgent
    {
        List<Monument> GetAllMonumenten();

        void Add(Monument monument);
        void Delete(int id);
    }
}