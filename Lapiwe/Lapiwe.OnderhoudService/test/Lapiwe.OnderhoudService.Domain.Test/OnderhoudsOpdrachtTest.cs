using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Lapiwe.OnderhoudService.Domain.Test
{
    [TestClass]
    public class OnderhoudsOpdrachtTest
    {

        [TestMethod]
        public void OnderhoudsOpdracht_instantiated()
        {
            var target = new OnderhoudsOpdracht
            {
                AanmeldDatum = DateTime.ParseExact("2016-11-30 10:59", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                Apk = true,
                AutoGuid = Guid.NewGuid(),
                ID = 1L,
                Kilometerstand = 10000,
                OpdrachtStatus = Status.Aangemeld,
                OpdrachtOmschrijving = "Testing instantiation",
                KlantGuid = Guid.NewGuid()
            };

            Assert.AreEqual(true, target.Apk);
            Assert.AreEqual(10000, target.Kilometerstand);
            Assert.AreEqual(Status.Aangemeld, target.OpdrachtStatus);
            Assert.AreEqual("Testing instantiation", target.OpdrachtOmschrijving);
        }
    }
}
