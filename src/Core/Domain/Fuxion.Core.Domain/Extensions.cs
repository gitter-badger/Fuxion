using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fuxion.Core.Domain
{
    public static class Extensions
    {
        public static IDisposable AsDisposable<T>(this T me, Action<T> actionOnDispose)
        {
            return new DisposableEnvelope<T>(me, actionOnDispose);
        }
        public static string ToJson(this object me)
        {
            try {
                return JToken.Parse(JsonConvert.SerializeObject(me)).ToString();
            }catch(Exception ex)
            {
                return "";
            }
        }
        public static T CloneWithJson<T>(this T me) {
            //var type = me.GetType() == typeof(T) ? typeof(T) : me.GetType();
            return (T)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(me, me.GetType(), new JsonSerializerSettings()), me.GetType());
            //return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(me));
        }
        public static string GetSignature(this MethodBase method,
            bool includeAccessModifiers = false,
            bool includeReturn = false,
            bool includeDeclarinType = true,
            bool useFullNames = false,
            bool includeParameters = true,
            bool includeParametersNames = false,
            Func<bool, bool, MethodBase, object, string> parametersFunction = null,
            object parametersFunctionArguments = null
            )
        {
            var res = new StringBuilder();
            // Access modifiers
            if (includeAccessModifiers)
            {
                if (method.IsPublic)
                    res.Append("public ");
                else if (method.IsPrivate)
                    res.Append("private ");
                else if (method.IsAssembly)
                    res.Append("internal ");
                if (method.IsFamily)
                    res.Append("protected ");
                if (method.IsStatic)
                    res.Append("static ");
                if (method.IsVirtual)
                    res.Append("virtual ");
                if (method.IsAbstract)
                    res.Append("abstract ");
            }
            // Return type
            if (includeReturn)
                res.Append(((MethodInfo)method).ReturnType.GetSignature(useFullNames) + " ");
            // Method name
            if (includeDeclarinType)
                res.Append(method.DeclaringType.GetSignature(useFullNames) + ".");
            res.Append(method.Name);
            // Generics arguments
            if (method.IsGenericMethod)
            {
                res.Append("<");
                var genericArgs = method.GetGenericArguments();
                for (var i = 0; i < genericArgs.Length; i++)
                    res.Append((i > 0 ? ", " : "") + genericArgs[i].GetSignature(useFullNames));
                res.Append(">");
            }
            // Parameters
            if (includeParameters)
            {
                res.Append("(");
                if (parametersFunction != null)
                {
                    res.Append(parametersFunction(useFullNames,includeParametersNames,method, parametersFunctionArguments));
                }
                else
                {
                    var pars = method.GetParameters();
                    for (var i = 0; i < pars.Length; i++)
                    {
                        var par = pars[i];
                        if (i == 0 && method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
                            res.Append("this ");
                        if (par.ParameterType.IsByRef)
                            res.Append("ref ");
                        else if (par.IsOut)
                            res.Append("out ");
                        res.Append(par.ParameterType.GetSignature(useFullNames));
                        if (includeParametersNames)
                            res.Append(" " + par.Name);
                        if (i < pars.Length - 1)
                            res.Append(", ");
                    }
                }
                res.Append(")");
            }
            return res.ToString();
        }
        public static string GetSignature(this Type type, bool useFullNames)
        {
            var nullableType = Nullable.GetUnderlyingType(type);
            if (nullableType != null)
                return nullableType.GetSignature(useFullNames) + "?";
            var typeName = useFullNames && !string.IsNullOrWhiteSpace(type.FullName) ? type.FullName : type.Name;
            if (!type.GetTypeInfo().IsGenericType)
                switch (type.Name)
                {
                    case "String": return "string";
                    case "Int32": return "int";
                    case "Decimal": return "decimal";
                    case "Object": return "object";
                    case "Void": return "void";
                    default:
                        {
                            return typeName;
                        }
                }

            var sb = new StringBuilder(typeName.Substring(0, typeName.IndexOf('`')));
            sb.Append('<');
            var first = true;
            foreach (var t in type.GenericTypeArguments)
            {
                if (!first)
                    sb.Append(',');
                sb.Append(GetSignature(t, useFullNames));
                first = false;
            }
            sb.Append('>');
            return sb.ToString();
        }
        class DisposableEnvelope<T> : IDisposable
        {
            public DisposableEnvelope(T obj, Action<T> actionOnDispose)
            {
                this.action = actionOnDispose;
                this.obj = obj;
            }
            T obj;
            Action<T> action;
            void IDisposable.Dispose()
            {
                action(obj);
            }
        }
    }
}
