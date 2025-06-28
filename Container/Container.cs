using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _bindings = new();

        public void Bind(Type interfaceType, Type implementationType)
        {
            if (!interfaceType.IsAssignableFrom(implementationType))
                throw new ArgumentException($"{implementationType.Name} does not implement {interfaceType.Name}");
            _bindings[interfaceType] = implementationType;
        }

        public T Get<T>()
        {
            var interfaceType = typeof(T);

            if (!_bindings.TryGetValue(interfaceType, out var implementationType))
                throw new InvalidOperationException($"No binding found for {interfaceType.Name}");

            return (T)Activator.CreateInstance(implementationType)!;
        }
    }
}
