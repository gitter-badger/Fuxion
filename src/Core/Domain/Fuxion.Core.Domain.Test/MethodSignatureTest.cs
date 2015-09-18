using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fuxion.Core.Domain;
using System.Diagnostics;

namespace Namespace
{
    [TestClass]
    public class MethodSignatureTest
    {
        [TestMethod]
        public void GetSignature_Full()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, true, true, true, true));
        }
        [TestMethod]
        public void GetSignature_Without_AccessModifiers()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(false, true, true, true, true, true));
        }
        [TestMethod]
        public void GetSignature_Without_Return()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, false, true, true, true, true));
        }
        [TestMethod]
        public void GetSignature_Without_DeclaringType()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, false, true, true, true));
        }
        [TestMethod]
        public void GetSignature_Without_FullNames()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, true, false, true, true));
        }
        [TestMethod]
        public void GetSignature_Without_Parameters()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, true, true, false, true));
        }
        [TestMethod]
        public void GetSignature_Without_ParametersNames()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, true, true, true, false));
        }
        [TestMethod]
        public void GetSignature_Minimal()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(false, false, false, false, false, false));
        }
        [TestMethod]
        public void GetSignature_Default()
        {
            var type = typeof(Class);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature());
        }
        [TestMethod]
        public void GetSignature_ExtensionMethod()
        {
            var type = typeof(StaticClass);
            var mets = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            foreach (var met in mets)
                Debug.WriteLine(met.GetSignature(true, true, true, true, true, true));
        }
    }
    public class Class
    {
        public void Method() { }

        // Return methods
        public string Method_Return_String() { return ""; }
        public int? Method_Return_NullableInt() { return 0; }
        public Value? Method_Return_NullableValue() { return default(Value); }
        public Data Method_Return_Data() { return null; }
        public Generic<string> Method_Return_GenericString() { return null; }
        public Generic<Data> Method_Return_GenericData() { return null; }

        // One parameter methods
        public void Method_Param_String(string param1) { }
        public void Method_Param_NullableInt(int? param1) { }
        public void Method_Param_NullableValue(Value? param1) { }
        public void Method_Param_Data(Data param1) { }
        public void Method_Param_GenericString(Generic<string> param1) { }
        public void Method_Param_GenericData(Generic<Data> param1) { }

        // Two parameters methods
        public void Method_TwoParams_String(string param1, string param2) { }
        public void Method_TwoParams_Data(Data param1, Data param2) { }
        public void Method_TwoParams_GenericString(Generic<string> param1, Generic<string> param2) { }
        public void Method_TwoParams_GenericData(Generic<Data> param1, Generic<Data> param2) { }
    }
    public static class StaticClass
    {
        public static void Method_Extension_String(this string me) { }
    }
    public class Data { }
    public class Generic<T> { }
    public struct Value { }
}
