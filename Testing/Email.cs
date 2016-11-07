using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    class Email
    {
        [Test]
        public void testEmailIsValid()
        {
            var email = new knatteligan.Domain.ValueObjects.Email("daniel@kuk.se");
            Assert.AreEqual("daniel@kuk.se", email.Value);
        }
        [Test]
        public void testEmailIsNotValid()
        {
            TestDelegate testdelegate = () => new knatteligan.Domain.ValueObjects.Email("daniel#¤@greger.se");
            Assert.That(testdelegate, Throws.TypeOf<Exception>());

        }
    }
}
