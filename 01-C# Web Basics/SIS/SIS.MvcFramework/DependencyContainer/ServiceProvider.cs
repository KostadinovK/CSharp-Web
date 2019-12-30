using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework.DependencyContainer
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly IDictionary<Type, Type> dependencyContainer = new ConcurrentDictionary<Type, Type>();

        public void Add<TSource, TDestination>()
            where TDestination : TSource
        {
            dependencyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (dependencyContainer.ContainsKey(type))
            {
                type = dependencyContainer[type];
            }

            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

            if(constructor == null)
            {
                return null;
            }

            var parameters = constructor.GetParameters();
            var parametersInstances = new List<object>();
            
            foreach (var parameter in parameters)
            {
                var parameterInstance = CreateInstance(parameter.ParameterType);
                parametersInstances.Add(parameterInstance);
            }

            var obj = constructor.Invoke(parametersInstances.ToArray());

            return obj;
        }
    }
}
