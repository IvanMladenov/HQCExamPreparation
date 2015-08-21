using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhoneBookTest
{
    using PhonebookMain;

    [TestClass]
    public class TestChangePhoneMethod
    {
        [TestMethod]
        public void TestChangeSinglePhoneMustReturnNumberOfChangedPhones()
        {
            var test = new PhonebookRepository();
            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });

            int changedPhones = test.ChangePhone("08895624", "08895628");

            Assert.AreEqual(1, changedPhones);

            // Number of phones should stay unchanged
            Assert.AreEqual(3, test.KeyNumberValueEntryCount);
        }

        [TestMethod]
        public void TestChangeMultiplePhoneMustReturnNumberOfChangedPhones()
        {
            var test = new PhonebookRepository();
            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });
            test.AddPhone("Gosho", new[] { "08895624", "08895625", "08895626" });

            int changedPhones = test.ChangePhone("08895624", "08895628");

            Assert.AreEqual(2, changedPhones);
        }

        [TestMethod]
        public void TestChangeUnexistingPhoneMustReturnNumberOfChangedPhones()
        {
            var test = new PhonebookRepository();
            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });

            int changedPhones = test.ChangePhone("08895629", "08895628");

            Assert.AreEqual(0, changedPhones);
        }
    }
}
