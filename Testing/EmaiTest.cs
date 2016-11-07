
using knatteligan.CustomExceptions;
using knatteligan.Domain.ValueObjects;

using NUnit.Framework;

namespace Testing {

    [TestFixture]
    public class EmailTest {

        [Test]
        public void TestEmailIsValid() {
            var email = new Email("daniel@kuk.se");
            Assert.AreEqual("daniel@kuk.se", email.Value);
        }

        [Test]
        public void TestEmailIsNotValid() {
            TestDelegate testdelegate = () => new Email("daniel#¤@greger.se");
            Assert.That(testdelegate, Throws.TypeOf<InvalidEmailException>());
        }

    }

}