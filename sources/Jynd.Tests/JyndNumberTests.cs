using NUnit.Framework;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndNumberTests
    {
        [Test]
        public void CanAccessDeserializedInt32()
        {
            string json = @"{""value"":123}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(123));
        }
    }
}