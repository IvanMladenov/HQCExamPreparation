using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TIcketOfficeTest
{
    using TicketOfficeMain;
    using TicketOfficeMain.Inerfaces;
    using TicketOfficeMain.Tickets;

    [TestClass]
    public class DeleteBusTicketsTest
    {
        [TestMethod]
        public void TestDeleteExistingBusTicketReturnsBusDeleted()
        {
            ITicketRepository repository = new TicketRepository();
            string cmdResult = repository.AddBusTicket("Sofia", "Varna", "BusExpress", new DateTime(2015, 1, 30, 12, 55, 00), 26.00M);

            string cmdResultDeleted = repository.DeleteBusTicket("Sofia", "Varna", "BusExpress", new DateTime(2015, 1, 30, 12, 55, 00));
            

            Assert.AreEqual("Bus deleted", cmdResultDeleted);
            Assert.AreEqual(0, repository.GetTicketsCount(TicketType.Bus));
            Assert.AreNotEqual(cmdResult, cmdResultDeleted);
        }

        [TestMethod]
        public void TestDeleteNotExistingBusTicketReturntsBusDoesNotExist()
        {
            ITicketRepository repository = new TicketRepository();
            repository.AddBusTicket("Sofia", "Varna", "BusExpress", new DateTime(2015, 1, 30, 12, 55, 00), 26.00M);

            string cmdResult = repository.DeleteBusTicket("Plovdiv", "Varna", "BusExpress", new DateTime(2015, 1, 30, 12, 55, 00));

            Assert.AreEqual("Bus does not exist", cmdResult);
            Assert.AreEqual(1, repository.GetTicketsCount(TicketType.Bus));
        }
    }
}
