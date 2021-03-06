﻿using System;
using System.Collections.Generic;
using DAL.Interfaces;
using Enities;

namespace Minor.Dag18.FrontEndOpdrachtApi.Tests
{
    internal class MonumentenRepositoryMock : IRepository<Monument, int>
    {
        public int FindAllCount { get; private set; }
        public List<int> GetByIdCalls { get; private set; }
        public List<Monument> AddCalls { get; private set; }
        public List<int> DeleteCalls { get; private set; }

        public MonumentenRepositoryMock()
        {
            GetByIdCalls = new List<int>();
            AddCalls = new List<Monument>();
            DeleteCalls = new List<int>();

        }

        public IEnumerable<Monument> FindAll()
        {
            FindAllCount++;

            return null;
        }

        public void Add(Monument item)
        {
            AddCalls.Add(item);
        }

        public void Delete(int id)
        {
            DeleteCalls.Add(id);
        }

        public Monument FindByKey(int key)
        {
            GetByIdCalls.Add(key);

            return null;
        }
    }
}