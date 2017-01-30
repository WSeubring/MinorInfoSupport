using Lapiwe.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.Common.Test.Entities
{
    public class Auto : DomainEntity
    {
        public Auto()
        {
        }

        public Auto(Guid guid) : base(guid)
        {
        }
    }
}
