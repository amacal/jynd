using NUnit.Framework;

namespace Jynd.Tests
{
    public class JyndWhiteTests
    {
        [Test]
        public void CanDeserializeEmptyObject()
        {
            string json = @"{ }";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
        }

        [Test]
        public void CanDeserializeOnePropertyObject()
        {
            string json = @"{ ""a"" : ""b"" }";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.a, Is.EqualTo("b"));
        }

        [Test]
        public void CanDeserializeTwoPropertiesObject()
        {
            string json = @"{ ""a"" : ""b"" , ""c"" : ""d"" }";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.a, Is.EqualTo("b"));
            Assert.That(data.c, Is.EqualTo("d"));
        }

        [Test]
        public void CanDeserializeEmptyArray()
        {
            string json = @"[ ]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.EqualTo(0));
        }

        [Test]
        public void CanDeserializeOneItemArray()
        {
            string json = @"[ 1 ]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.EqualTo(1));
            Assert.That(data[0], Is.EqualTo(1));
        }

        [Test]
        public void CanDeserializeTwoItemsArray()
        {
            string json = @"[ 1 , 2 ]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.EqualTo(2));
            Assert.That(data[0], Is.EqualTo(1));
            Assert.That(data[1], Is.EqualTo(2));
        }

        [Test]
        public void CanDeserializePolimorphicArray()
        {
            string json = @"[ 1 , ""a"" , true , false , null , -1.023 , { }, [ ] ]";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data, Is.Not.Null);
            Assert.That(data.Length, Is.EqualTo(8));
            Assert.That(data[0], Is.EqualTo(1));
            Assert.That(data[1], Is.EqualTo("a"));
            Assert.That(data[2], Is.EqualTo(true));
            Assert.That(data[3], Is.EqualTo(false));
            Assert.That(data[4], Is.EqualTo(null));
            Assert.That(data[5], Is.EqualTo(-1.023));
            Assert.That(data[6], Is.Not.Null);
            Assert.That(data[7], Is.Not.Null);
            Assert.That(data[7].Length, Is.EqualTo(0));
        }
    }
}
