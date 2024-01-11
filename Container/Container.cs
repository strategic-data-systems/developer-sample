using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly Dictionary<Type, Type> containerMap = new();

        /// <summary>
        /// Binds the Interface to a type
        /// </summary>
        /// <param name="interfaceType">Interface to be mapped for the implementationType</param>
        /// <param name="implementationType">Type to be mapped for the interfaceType</param>
        /// <exception cref="InvalidCastException">If the type doesnt implement the interface, InvalidCastException is thrown.</exception>
        public void Bind(Type interfaceType, Type implementationType)
        {
            // Ensure the interfaceType is actually an interface
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"The provided interfaceType {interfaceType.FullName} is not an interface.");
            }

            if (implementationType.IsAbstract || implementationType.IsInterface)
            {
                throw new ArgumentException($"The provided implementationType: {implementationType.FullName} is a abstract or an interface and cannot be bind.");
            }

            //Check if the type is inhertied from the other type
            if (!interfaceType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException($"The implementation type {implementationType.FullName} does not implement the interface {interfaceType.FullName}.");
            }
            containerMap[interfaceType] = implementationType;
        }

        /// <summary>
        /// Gets the object of the class based on the registration
        /// </summary>
        /// <typeparam name="T">Interface type</typeparam>
        /// <returns>Object for the given interface type</returns>
        /// <exception cref="DependencyResolutionException">If there is no binding found for the interface type, DependencyResolutionException is thrown</exception>
        public T Get<T>()
        {
            var interfaceType = typeof(T);
            containerMap.TryGetValue(interfaceType, out Type implementationType);
            if (implementationType == null)
            {
                throw new DependencyResolutionException($"Unable to resolve the Type:{interfaceType.FullName}.");
            }

            return (T)Activator.CreateInstance(implementationType);
        }
    }
}