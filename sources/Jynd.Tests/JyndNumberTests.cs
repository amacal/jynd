using NUnit.Framework;
using System.Numerics;

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
            Assert.That(data.value, Is.TypeOf<int>());
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
            Assert.That(data.value, Is.TypeOf<int>());
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
        public void CanAccessDeserializedInt32MaxValuePlusOneAsInt64()
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
        public void CanAccessDeserializedInt64()
        {
            string json = @"{""value"":922337203685}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(922337203685L));
            Assert.That(data.value, Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt64WithNegation()
        {
            string json = @"{""value"":-922337203685}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-922337203685L));
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

        [Test]
        public void CanAccessDeserializedInt64MaxValuePlusOneAsBigInteger()
        {
            string json = @"{""value"":9223372036854775808}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value > 9223372036854775807L, Is.True);
            Assert.That(data.value, Is.TypeOf<BigInteger>());
        }

        [Test]
        public void CanAccessDeserializedInt64MinValueMinusOneAsBigInteger()
        {
            string json = @"{""value"":-9223372036854775809}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value < -9223372036854775808L, Is.True);
            Assert.That(data.value, Is.TypeOf<BigInteger>());
        }

        [Test]
        public void CanAccessDeserializedBigInteger()
        {
            string json = @"{""value"":92233720368532743274237423742342134}";
            dynamic data = JyndConvert.Deserialize(json);

            BigInteger value = BigInteger.Parse("92233720368532743274237423742342134");
            Assert.That(data.value, Is.EqualTo(value));
        }

        [Test]
        public void CanAccessDeserializedBigIntegerWithNegation()
        {
            string json = @"{""value"":-92233720368532743274237423742342134}";
            dynamic data = JyndConvert.Deserialize(json);

            BigInteger value = BigInteger.Parse("-92233720368532743274237423742342134");
            Assert.That(data.value, Is.EqualTo(value));
        }

        [Test]
        public void CanAccessDeserializedDouble()
        {
            string json = @"{""value"":123.456}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(123.456));
            Assert.That(data.value, Is.TypeOf<double>());
        }

        [Test]
        public void CanAccessDeserializedDoubleWithNegation()
        {
            string json = @"{""value"":-123.456}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(-123.456));
            Assert.That(data.value, Is.TypeOf<double>());
        }

        [Test]
        public void CanAccessDeserializedInt32ExplicitlyUsingObject()
        {
            string json = @"{""value"":123}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt32("value"), Is.EqualTo(123));
            Assert.That(data.GetInt32("value"), Is.TypeOf<int>());
        }

        [Test]
        public void CanAccessDeserializedInt32ExplicitlyUsingArray()
        {
            string json = @"[123]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt32(0), Is.EqualTo(123));
            Assert.That(data.GetInt32(0), Is.TypeOf<int>());
        }

        [Test]
        public void CanAccessDeserializedInt32ExplicitlyAsNull()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt32("value"), Is.Null);
        }

        [Test]
        public void CanAccessDeserializedInt64ExplicitlyUsingObject()
        {
            string json = @"{""value"":123}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt64("value"), Is.EqualTo(123));
            Assert.That(data.GetInt64("value"), Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt64ExplicitlyUsingArrayAsInt64()
        {
            string json = @"[123]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt64(0), Is.EqualTo(123));
            Assert.That(data.GetInt64(0), Is.TypeOf<long>());
        }

        [Test]
        public void CanAccessDeserializedInt64ExplicitlyAsNull()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetInt64("value"), Is.Null);
        }

        [Test]
        public void CanAccessDeserializedBigIntegerExplicitlyUsingObject()
        {
            string json = @"{""value"":123}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetBigInteger("value"), Is.EqualTo(new BigInteger(123)));
            Assert.That(data.GetBigInteger("value"), Is.TypeOf<BigInteger>());
        }

        [Test]
        public void CanAccessDeserializedBigIntegerExplicitlyUsingArrayAsInt64()
        {
            string json = @"[123]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetBigInteger(0), Is.EqualTo(new BigInteger(123)));
            Assert.That(data.GetBigInteger(0), Is.TypeOf<BigInteger>());
        }

        [Test]
        public void CanAccessDeserializedBigIntegerExplicitlyAsNull()
        {
            string json = @"{""value"":null}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.GetBigInteger("value"), Is.Null);
        }
    }
}