using Enities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Minor.Dag18.FrontEndOpdracht.Agents
{ 
    internal class DummyMonumentenAgent 
    {
        private List<Monument> _monumenten { get; set; }

        public DummyMonumentenAgent()
        {
            _monumenten = new List<Monument>();
            _monumenten.Add(new Monument() { Naam = "Monument1", ID=1});
            _monumenten.Add(new Monument() { Naam = "Monument2", ID=2});
        }

        public void Add(Monument monument)
        {
            _monumenten.Add(monument);
        }

        public List<Monument> GetAllMonumenten()
        {
            return _monumenten;    
        }

        public void Delete(int id)
        {
            var monument = _monumenten.Single(m => m.ID == id);
            _monumenten.Remove(monument);
        }
    }
}