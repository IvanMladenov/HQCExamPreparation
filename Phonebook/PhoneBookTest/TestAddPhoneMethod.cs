using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhoneBookTest
{
    using PhonebookMain;

    [TestClass]
    public class TestAddPhoneMethod
    {
        [TestMethod]
        public void TestAddUniquePhonebookEntryWithSingleNumber()
        {
           var test = new PhonebookRepository();

           bool isNewNumber = test.AddPhone("Pesho", new[] { "08895624" });
            int entriesCount = test.PhoneEntrySortedCount;
            int numbersCount = test.KeyNumberValueEntryCount;


            Assert.AreEqual(true, isNewNumber);
            Assert.AreEqual(1, entriesCount);
        }

        [TestMethod]
        public void TestAddUniquePhonebookEntryWithMultipleNumbers()
        {
            var test = new PhonebookRepository();

            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });
            int numbersCount = test.KeyNumberValueEntryCount;

            Assert.AreEqual(3, numbersCount);
        }

        [TestMethod]
        public void TestAddDuplicatePhonebookEntryWithDiferentNumberMustMergeNumbers()
        {
            var test = new PhonebookRepository();

            test.AddPhone("Pesho", new[] { "08895624" });
            bool isNewNumber = test.AddPhone("Pesho", new[] { "08895625" });

            int entriesCount = test.PhoneEntrySortedCount;
            int numbersCount = test.KeyNumberValueEntryCount;

            Assert.AreEqual(false, isNewNumber);
            Assert.AreEqual(1, entriesCount);
            Assert.AreEqual(2, numbersCount);
        }

        [TestMethod]
        public void TestAddDuplicatePhonebookEntryWithSameNumberMustChangeNothing()
        {
            var test = new PhonebookRepository();

            test.AddPhone("Pesho", new[] { "08895624" });
            test.AddPhone("Pesho", new[] { "08895624" });

            int entriesCount = test.PhoneEntrySortedCount;
            int numbersCount = test.KeyNumberValueEntryCount;

            Assert.AreEqual(1, entriesCount);
            Assert.AreEqual(1, numbersCount);
        }
    }
}
