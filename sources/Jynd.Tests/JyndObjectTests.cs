using NUnit.Framework;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndObjectTests
    {
        [Test]
        public void CanDeserializeEmptyObject()
        {
            string json = @"{}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
        }

        [Test]
        public void CanAccessDeserializedSingleProperty()
        {
            string json = @"{""value"":""a""}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo("a"));
        }

        [Test]
        public void CanAccessDeserializedMultiProperty()
        {
            string json = @"{""value"":""a"",""data"":""b""}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo("a"));
            Assert.That(data.data, Is.EqualTo("b"));
        }

        [Test]
        public void CanAccessDeserializedNestedObject()
        {
            string json = @"{""value"":{""data"":""b""}}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value.data, Is.EqualTo("b"));
        }

        [Test]
        public void CanAccessDeserializedNestedObjectWithSimilarNames()
        {
            string json = @"{""value"":{""value"":""b""}}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value.value, Is.EqualTo("b"));
        }

        [Test]
        public void CanAccessDeserializedNestedObjectWithWithPropertyAfterIt()
        {
            string json = @"{""value"":{""value"":""b""},""extra"":""c""}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.extra, Is.EqualTo("c"));
        }

        [Test]
        public void CanAccessDeserializedNestedSecondObject()
        {
            string json = @"{""value"":{""value"":""b""},""second"":{""value"":""c""}}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.second.value, Is.EqualTo("c"));
        }

        [Test]
        public void CanAccessDeserializedPropertyAfterEscapedText()
        {
            string json = @"{""escaped"":""102\\"",""value"":1}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(1));
        }

        [Test]
        public void CanAccessPropertyUsingIndexer()
        {
            string json = @"{""value"":""a""}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data["value"], Is.EqualTo("a"));
        }

        [Test]
        public void ThrowsExceptionOnUnknownProperty()
        {
            string json = @"{""value"":""a"",""data"":""b""}";
            dynamic data = JyndConvert.Deserialize(json);

            TestDelegate callback = () => data.missing.ToString();

            Assert.That(callback, Throws.InstanceOf<JyndException>());
        }

        [Test]
        public void ThrowsExceptionOnUnknownPropertyAccessedByIndexer()
        {
            string json = @"{""value"":""a"",""data"":""b""}";
            dynamic data = JyndConvert.Deserialize(json);

            TestDelegate callback = () => data["missing"].ToString();

            Assert.That(callback, Throws.InstanceOf<JyndException>());
        }
    }
}