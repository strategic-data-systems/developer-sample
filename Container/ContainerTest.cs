using System;
using System.Collections.Generic;
using Xunit;

namespace DeveloperSample.Container
{
    internal interface IContainerTestInterface
    {
    }

    internal class ContainerTestClass : IContainerTestInterface
    {
    }

    internal class Foo
    {
    }

    public class ContainerTest
    {
        [Fact()]
        public void CanBindAndGetService()
        {
            var container = new Container();
            container.Bind(typeof(IContainerTestInterface), typeof(ContainerTestClass));
            var testInstance = container.Get<IContainerTestInterface>();
            Assert.IsType<ContainerTestClass>(testInstance);
        }

        [Fact()]
        public void CannotBindWhenTypesAreNotRelated()
        {
            //Arrange
            var container = new Container();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => container.Bind(typeof(IContainerTestInterface), typeof(Foo)));
            Assert.Throws<DependencyResolutionException>(() => container.Get<IContainerTestInterface>());
        }

        [Fact()]
        public void CannotBindWhenSourceTypeIsNotInterface()
        {
            //Arrange
            var container = new Container();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => container.Bind(typeof(Foo), typeof(Foo)));
            Assert.Throws<DependencyResolutionException>(() => container.Get<Foo>());
        }

        [Fact()]
        public void CannotBindWhenDestTypeIsNotClass()
        {
            //Arrange
            var container = new Container();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => container.Bind(typeof(IContainerTestInterface), typeof(IContainerTestInterface)));
            Assert.Throws<DependencyResolutionException>(() => container.Get<Foo>());
        }
    }
}