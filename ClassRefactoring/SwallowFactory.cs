namespace DeveloperSample.ClassRefactoring
{
    public class SwallowFactory
    {
        /// <summary>
        /// Factory generates a Swallow object based on the swallow type.
        /// </summary>
        /// <param name="swallowType">Swallow type</param>
        /// <returns>Swallow object</returns>
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }
}
