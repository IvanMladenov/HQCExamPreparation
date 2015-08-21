using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhoneBookTest
{
    using PhonebookMain;

    [TestClass]
    public class TestListMethod
    {
        [TestMethod]
        public void TestMethodWithInvalidStartPositionShouldReturnNull()
        {
            var test = new PhonebookRepository();
            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });

            var result=test.ListEntries(2, 1);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void TestMethodWithValidStartPositionAndCount()
        {
            var test = new PhonebookRepository();
            test.AddPhone("Pesho", new[] { "08895624", "08895625", "08895626" });
            test.AddPhone("Gosho", new[] { "08895624", "08895625", "08895626" });
            test.AddPhone("Mara", new[] { "08895624", "08895625", "08895626" });

            var result = test.ListEntries(0, 3);
            int returnedEntriesCount = result.Length;

            Assert.AreEqual(3, returnedEntriesCount);
        }
    }
}
