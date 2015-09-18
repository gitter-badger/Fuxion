using System;
using System.Collections.Generic;
using Fuxion.Core.Domain.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuxion.Core.Domain.Test
{
    //public class StringFactory : IFactory
    //{
    //    public object Create(Type type)
    //    {
    //        return type == typeof (string) ? "New string created" : null;
    //    }

    //    public IEnumerable<T> GetAllInstances<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //public class IntFactory : IFactory
    //{
    //    public object Create(Type type)
    //    {
    //        return type == typeof(int) ? (object)123 : null;
    //    }

    //    public IEnumerable<T> GetAllInstances<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    //[TestClass]
    //public class FactoryTest
    //{
    //    [TestMethod]
    //    public void WhenHaveTwoElementsInPipe_CreationContinueUntilAnElementCreateIt()
    //    {
    //        Factory.AddToPipe(new IntFactory());
    //        Factory.AddToPipe(new StringFactory());
    //        Assert.IsTrue(Factory.Create<string>() != null);
    //        Assert.IsTrue(Factory.Create<int>() == 123);
    //    }
    //}
}
