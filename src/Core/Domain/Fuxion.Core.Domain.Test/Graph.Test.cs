using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Fuxion.Core.Domain.Helpers;

namespace Fuxion.Core.Domain.Test
{
    [TestClass]
    public class GraphTest
    {
        public static Graph<string> GetGraph()
        {
            var g = new Graph<string>();
            // It performs better than call many times to AddEdge method because HasCycles method only will be called once
            g.AddEdges(
                new[] { "Admin", "Manage" },
                new[] { "Manage", "Create" },
                new[] { "Manage", "Delete" },
                new[] { "Create", "Edit" },
                new[] { "Delete", "Edit" },
                new[] { "Edit", "Read" });
            return g;
        }
        [TestMethod]
        public void WhenGraph_CheckDescendants()
        {
            var g = GetGraph();
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Read"), new string[] { }), "Descendants of READ are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Edit"), new[] { "Read" }), "Descendants of EDIT are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Create"), new[] { "Edit", "Read" }), "Descendants of CREEATE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Delete"), new[] { "Edit", "Read" }), "Descendants of DELETE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Manage"), new[] { "Create", "Delete", "Edit", "Read" }), "Descendants of MANAGE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetDescendants("Admin"), new[] { "Manage", "Create", "Delete", "Edit", "Read" }), "Descendants of ADMIN are incorrect");
        }
        [TestMethod]
        public void WhenGraph_CheckAscendants()
        {
            var g = GetGraph();
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Read"), new[] { "Edit", "Create", "Delete", "Manage", "Admin" }), "Ascendants of READ are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Edit"), new[] { "Create", "Delete", "Manage", "Admin" }), "Ascendants of EDIT are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Create"), new[] { "Manage", "Admin" }), "Ascendants of CREATE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Delete"), new[] { "Manage", "Admin" }), "Ascendants of DELETE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Manage"), new[] { "Admin" }), "Ascendants of MANAGE are incorrect");
            Assert.IsTrue(Enumerable.SequenceEqual(g.GetAscendants("Admin"), new string[] { }), "Ascendants of ADMIN are incorrect");
        }
        [TestMethod]
        public void WhenGraph_DetectCycles()
        {
            var g = GetGraph();
            Assert.IsFalse(g.HasCycles());
            try
            {
                g.AddEdge("Read", "Manage");
                Assert.Fail();
            }
            catch (GraphCyclicException) { }
            g.AllowCycles = true;
            g.AddEdge("Read", "Manage");
            Assert.IsTrue(g.HasCycles());
        }
    }
}
