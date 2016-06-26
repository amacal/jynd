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

        [Test]
        public void CanAccessDeserializedInt32AsInt64()
        {
            string json = @"{""value"":123}";
            dynamic data = JyndConvert.Deserialize(json);

            long value = data.value;
            Assert.That(value, Is.EqualTo(123));
        }

        [Test]
        public void CanAccessDeserializedInt32WithNegation()
        {
            string json = @"{""value"":-123}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-123));
        }

        [Test]
        public void CanAccessDeserializedInt32MaxValue()
        {
            string json = @"{""value"":2147483647}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(2147483647));
            Assert.That(data.value, Is.TypeOf<int>());
        }

        [Test]
        public void CanAccessDeserializedInt32MinValue()
        {
            string json = @"{""value"":-2147483648}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-2147483648));
            Assert.That(data.value, Is.TypeOf<int>());
        }

        [Test]
        public void CanAccessDeserializedInt32MaxValuePlusOneAndAsInt64()
        {
            string json = @"{""value"":2147483648}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(2147483648L));
            Assert.That(data.value, Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt32MinValueMinusOneAsInt64()
        {
            string json = @"{""value"":-2147483649}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-2147483649L));
            Assert.That(data.value, Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt64MaxValue()
        {
            string json = @"{""value"":9223372036854775807}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(9223372036854775807L));
            Assert.That(data.value, Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt64MinValue()
        {
            string json = @"{""value"":-9223372036854775808}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-9223372036854775808L));
            Assert.That(data.value, Is.TypeOf<long>());
        }
    }
}