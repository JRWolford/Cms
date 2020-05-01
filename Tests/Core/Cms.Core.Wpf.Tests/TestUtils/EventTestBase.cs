using NUnit.Framework;
using Prism.Events;

namespace Cms.Core.Wpf.Tests.TestUtils
{
    /// <summary>
    ///     The base test class for event class testing.
    /// </summary>
    /// <typeparam name="TEvent">
    ///     The event class being tested.
    /// </typeparam>
    /// <typeparam name="TPayload">
    ///     The payload type of the event class being tested.
    /// </typeparam>
    public abstract class EventTestBase<TEvent, TPayload> where TEvent : class
    {
        /// <summary>
        ///     Asserts that the event inherits from the <see cref="PubSubEvent"/>.
        /// </summary>
        [Test]
        public void EventShouldInheritFromPubSub() =>
            Assert.That(typeof(TEvent).IsSubclassOf(typeof(PubSubEvent<TPayload>)));
    }
}
