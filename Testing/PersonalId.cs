using System;
using System.Collections.Generic;
using System.Globalization;
using knatteligan.Domain.Entities;
using knatteligan.Services;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class PersonalId
    {
        [Test]
        public void CheckPersonalIdValid()
        {
            var doa = new DateTime(1996, 11, 07);

            var pId = new knatteligan.Domain.ValueObjects.PersonalId(doa, "1136");
            Assert.AreEqual("1136", pId.LastFour);
            Assert.AreEqual(doa, pId.DateOfBirth);
        }

        [Test]
        public void CheckPersonalIdNotValid()
        {
            var doa = new DateTime(1996, 11, 07);

            TestDelegate testDeligate = () => new knatteligan.Domain.ValueObjects.PersonalId(doa, "sdfgh");
            Assert.That(testDeligate, Throws.TypeOf<knatteligan.Domain.ValueObjects.InvalidPersonalId>());
        }
    }
}