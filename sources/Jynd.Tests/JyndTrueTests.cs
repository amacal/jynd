using NUnit.Framework;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndTrueTests
    {
        [Test]
        public void CanAccessDeserializedTrue()
        {
            string json = @"{""value"":true}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.True);
        }
    }
}