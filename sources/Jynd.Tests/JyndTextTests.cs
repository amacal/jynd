using NUnit.Framework;
using System.Collections;

namespace Jynd.Tests
{
    [TestFixture]
    public class JyndTextTests
    {
        [Test]
        public void CanAccessDeserializedText()
        {
            string json = @"{""value"":""abc""}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo("abc"));
        }

        [Test]
        [TestCaseSource("EscapeCases")]
        public void CanAccessDeserializedTextWithEscapedCharacters(dynamic fixture)
        {
            string json = $@"{{""value"":""{fixture.source}""}}";
            dynamic data = JyndConvert.Deserialize(json);

            Assert.That(data.value, Is.EqualTo(fixture.result));
        }

        public static IEnumerable EscapeCases()
        {
            yield return new
            {
                source = @"ab\u00fcc",
                result = "abüc"
            };

            yield return new
            {
                source = @"ab\""c",
                result = "ab\"c"
            };

            yield return new
            {
                source = @"ab\/c",
                result = "ab/c"
            };

            yield return new
            {
                source = @"ab\bc",
                result = "ab\bc"
            };

            yield return new
            {
                source = @"ab\fc",
                result = "ab\fc"
            };

            yield return new
            {
                source = @"ab\nc",
                result = "ab\nc"
            };

            yield return new
            {
                source = @"ab\rc",
                result = "ab\rc"
            };

            yield return new
            {
                source = @"ab\tc",
                result = "ab\tc"
            };
        }
    }
}