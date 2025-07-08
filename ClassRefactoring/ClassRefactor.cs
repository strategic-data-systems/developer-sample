using System;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }

    public class Swallow
    {
        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
            Load= SwallowLoad.None;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            Load = load;
        }

        // Basha - I am using here Switch statement to avoid code cognitive issues and improves code readability and easy for  mocking 
        public double GetAirspeedVelocity()
        {
            return (Type,Load) switch
            {
                  (SwallowType.African,SwallowLoad.None) => 22,  
                  (SwallowType.African ,SwallowLoad.Coconut) => 18,
                  (SwallowType.European ,SwallowLoad.None) => 20,
                  (SwallowType.European ,SwallowLoad.Coconut) => 16,
                   _=> throw new InvalidOperationException("SwalloType and Load is not exists here")
            };
        }
    }
}
