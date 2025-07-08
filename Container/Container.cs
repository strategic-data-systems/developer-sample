using System;
using system.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        //Basha - I am making the dictionary to store binding mappings here
        private readonly IDictionary<Type, Type> _bindingMappings= new();
        
        public void Bind(Type interfaceType, Type implementationType) 
        {
            if(!interfaceType.IsAssignableFrom(implementationType))
                throw new ArgumentException($"Implementation Type  {implementationType.Name} does not Implement Interfacetype {interfaceType.Name} ");
            
            __bindingMappings[interfaceType]= implementationType;
        };
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }
        private object Get(Type type)
        {
            if(!_bindingMappings.TryGetValue(type, out var implementationType))
                throw new InvalidOperationException("Binding mappings not found");
            
            var construct = implementationType.GetConstructors()[0];
            var paras= construct.GetParameters();
            var instance= new object[paras.Length];

            for(int i=0;i< paras.Length;i++)
                instance[i]=Get(paras[i].ParameterType);

            return Activator.CreateInstance(implementationType, instance);
        }
    }
}
