using NUnit.Framework;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndFalseTests
    {
        [Test]
        public void CanAccessDeserializedFalse()
        {
            string json = @"{""value"":false}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.False);
        }
    }
}