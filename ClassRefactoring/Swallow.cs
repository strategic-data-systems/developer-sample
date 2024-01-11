using System;

namespace DeveloperSample.ClassRefactoring
{
    public class Swallow
    {
        #region Properties
        public SwallowType SwallowType { get; }
        public SwallowLoadType SwallowLoadType { get; private set; }
        #endregion

        #region Constructor
        public Swallow(SwallowType swallowType)
        {
            SwallowType = swallowType;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Applies the given load to the Swallow
        /// </summary>
        /// <param name="load">Load type</param>
        public void ApplyLoad(SwallowLoadType load)
        {
            SwallowLoadType = load;
        }

        /// <summary>
        /// Gets the velocity of the Swallow
        /// </summary>
        /// <returns>Velocity</returns>
        /// <exception cref="InvalidOperationException">For invalid swallow type, InvalidOperationException is thrown</exception>
        public double GetAirspeedVelocity()
        {
            //Switch expressions in C# 8.0 are more readable
            return (SwallowType, SwallowLoadType) switch
            {
                (SwallowType.African, SwallowLoadType.None) => 22,
                (SwallowType.African, SwallowLoadType.Coconut) => 18,
                (SwallowType.European, SwallowLoadType.None) => 20,
                (SwallowType.European, SwallowLoadType.Coconut) => 16,
                _ => throw new InvalidOperationException("Invalid swallow type or load type.")
            };
        }
        #endregion
    }
}