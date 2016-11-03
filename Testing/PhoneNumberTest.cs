using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using knatteligan.Domain.ValueObjects;

namespace Testing
{
    [TestFixture]
    public class PhoneNumberTest
    {
        [Test]
        public void CheckValidPhoneNumber()
        {

            var phoneNumber = new PhoneNumber("0733209064");
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
    }
}
