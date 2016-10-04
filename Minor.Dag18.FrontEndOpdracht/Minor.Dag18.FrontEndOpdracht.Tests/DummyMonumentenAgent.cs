using System;
using System.Collections.Generic;
using Agents;
using Minor.Dag18.FrontEndOpdracht.Models;
using System.Linq;

namespace Minor.Dag18.FrontEndOpdracht.Tests
{
    internal class DummyMonumentenAgent : IMonumentenAgent
    {
        private List<Monument> _monumenten { get; set; }

        public DummyMonumentenAgent()
        {
            _monumenten = new List<Monument>();
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