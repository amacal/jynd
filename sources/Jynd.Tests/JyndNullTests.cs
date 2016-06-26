using NUnit.Framework;
using System;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndNullTests
    {
        [Test]
        public void CanAccessDeserializedNull()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.Null);
        }

        [Test]
        public void CanAccessDeserializedNullAsInt32()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            Int32? value = data.value;
            Assert.That(value, Is.Null);
        }

        [Test]
        public void CanAccessDeserializedNullAsString()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            string value = data.value;
            Assert.That(value, Is.Null);
        }

        [Test]
        public void CanAccessDeserializedNullAsBool()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            bool? value = data.value;
            Assert.That(value, Is.Null);
        }

        [Test]
        public void CanAccessDeserializedNullAsArray()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            dynamic[] value = data.value;
            Assert.That(value, Is.Null);
        }

        [Test]
        public void CanAccessDeserializedNullAsObject()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            dynamic value = data.value;
            Assert.That(value, Is.Null);
        }
    }
}