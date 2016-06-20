using NUnit.Framework;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndArrayTests
    {
        [Test]
        public void CanAccessDeserializedNestedEmptyArray()
        {
            string json = @"{""value"":[]}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.Empty);
        }

        [Test]
        public void CanAccessDeserializedNestedArray()
        {
            string json = @"{""value"":[1,""a""]}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Has.Length.EqualTo(2));
            Assert.That(data.value[0], Is.EqualTo(1));
            Assert.That(data.value[1], Is.EqualTo("a"));
        }

        [Test]
        public void CanAccessDeserializedNestedArrayWithObjectInside()
        {
            string json = @"{""value"":[{""data"":3}]}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Has.Length.EqualTo(1));
            Assert.That(data.value[0], Is.Not.Null);
            Assert.That(data.value[0].data, Is.EqualTo(3));
        }

        [Test]
        public void CanAccessDeserializedNestedArrayWithForNumbersInside()
        {
            string json = @"{""value"":[0,1,2,3]}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Has.Length.EqualTo(4));
            Assert.That(data.value[0], Is.EqualTo(0));
            Assert.That(data.value[1], Is.EqualTo(1));
            Assert.That(data.value[2], Is.EqualTo(2));
            Assert.That(data.value[3], Is.EqualTo(3));
        }
    }
}