using System;
using System.Collections.Generic;
using Fuxion.Core.Domain.Repositories;
using Fuxion.Core.Infrastructure.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Waf.Parking.Domain;
using Fuxion.Core.Infrastructure.MongoDB;

namespace Fuxion.Core.Domain.Test
{
    [TestClass]
    public class MongoEventSourcedRepositoryTest
    {
        [TestMethod]
        public async Task Demo()
        {
            var rep = new MongoEventSourcedRepository<AccessList>();
            await rep.SaveAsync(new AccessList(Guid.NewGuid(),"oka"), "correlationDemo");
        }

        [TestMethod]
        public async Task WhenInsert_CanRetrieveIt()
        {
            var rep = new MongoEventSourcedRepository<AccessList>();
            var id = Guid.NewGuid();
            await rep.SaveAsync(new AccessList(id, "oka"), "correlationDemo");
            var res = await rep.GetAsync(id);
            Assert.IsTrue(res.Name == "oka");
        }
    }
}
