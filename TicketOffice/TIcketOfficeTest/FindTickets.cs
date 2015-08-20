using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TIcketOfficeTest
{
    using System.Runtime.InteropServices;

    using TicketOfficeMain;
    using TicketOfficeMain.Inerfaces;

    [TestClass]
    public class FindTickets
    {
        [TestMethod]
        public void TestFindSingleTicket()
        {
            ITicketRepository repository = new TicketRepository();
            repository.AddBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 1, 30, 12, 55, 00), 25m);

            string actualOutput = repository.FindTickets("Sofia", "Varna");
            string expectedOutput = "[30.01.2015 12:55|bus|25.00]";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestFindMultipleTickets()
        {
            ITicketRepository repository = new TicketRepository();
            repository.AddBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 1, 30, 12, 55, 00), 25m);
            repository.AddAirTicket("FX215", "Sofia", "Varna", "Bulgaria Air", new DateTime(2015, 1, 30, 12, 55, 00), 130.50M);
            repository.AddTrainTicket("Sofia", "Varna", new DateTime(2015, 1, 30, 12, 55, 00), 55m, 35m);

            string actualOutput = repository.FindTickets("Sofia", "Varna");
            string expectedOutput =
                "[30.01.2015 12:55|bus|25.00] [30.01.2015 12:55|flight|130.50] [30.01.2015 12:55|train|55.00]";
            
            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void TestFindNonExistingTickets()
        {
            ITicketRepository repository = new TicketRepository();
            repository.AddBusTicket("Sofia", "Varna", "Etap Address", new DateTime(2015, 1, 30, 12, 55, 00), 25m);
            repository.AddAirTicket("FX215", "Sofia", "Varna", "Bulgaria Air", new DateTime(2015, 1, 30, 12, 55, 00), 130.50M);
            repository.AddTrainTicket("Sofia", "Varna", new DateTime(2015, 1, 30, 12, 55, 00), 55m, 35m);

            string actualOutput = repository.FindTickets("Plovdiv", "Varna");
            string expectedOutput = "No matches";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
