namespace TicketOfficeMain.Inerfaces
{
    using System;

    using TicketOfficeMain.Tickets;

    public interface ITicketRepository
    {
        /// <summary>
        /// Add air ticket to repository with given, flight number, start destination, 
        /// end destination, airline company, date and time of the flight and price.
        /// </summary>
        /// <param name="flightNumber">The number of the flight which is unique.</param>
        /// <param name="from">Start destination.</param>
        /// <param name="to">End destination</param>
        /// <param name="airline">Name of the airline company.</param>
        /// <param name="dateTime">Date and time of the flight</param>
        /// <param name="price">Ticket price.</param>
        /// <returns>Message "Flight created" if the flight is added succesfully, or "Duplicate flight"
        /// if the flight already exist.</returns>
        string AddAirTicket(
            string flightNumber, 
            string from, 
            string to, 
            string airline, 
            DateTime dateTime, 
            decimal price);

        string DeleteAirTicket(string flightNumber);

        string AddTrainTicket(string from, string to, DateTime dateTime, decimal price, decimal studentPrice);

        string DeleteTrainTicket(string from, string to, DateTime dateTime);

        string AddBusTicket(string from, string to, string travelCompany, DateTime dateTime, decimal price);

        /// <summary>
        /// Delete ticket from repository by generating unique key of start destination, end destination,
        /// name of the travel company and date and time of the travel.
        /// </summary>
        /// <param name="from">Start destination.</param>
        /// <param name="to">End destination.</param>
        /// <param name="travelCompany">Name of the travel company.</param>
        /// <param name="dateTime">Date and time of the trip.</param>
        /// <returns>Message describes if the ticket is removed successfully.
        /// "Bus deleted" in case of success or “Bus does not exist” in case of not such key in repository. </returns>
        string DeleteBusTicket(string from, string to, string travelCompany, DateTime dateTime);

        /// <summary>
        /// Finds in the repository all tickets from given departure town/airport (from)
        ///  to given arrival town/airport (to), by generating key FromTo.
        /// </summary>
        /// <param name="from">Departure place.</param>
        /// <param name="to">Arrival place.</param>
        /// <returns>Returns collection as string matching Departure and Arrival places ordered by date and type, 
        /// then by type and then by price all accending. </returns>
        string FindTickets(string from, string to);

        /// <summary>
        /// Finds all tickets from the repository by given departure time interval (inclusive range). 
        /// </summary>
        /// <param name="startDateTime">Start date and time of the searching interval.</param>
        /// <param name="endDateTime">End date and time of the searching interval.</param>
        /// <returns>
        /// As a result the command prints all matching tickets on a single line, 
        /// separated by spaces, in format [date and time; type; price]
        /// where type is either “flight” or “bus” or “train” ordered by date and time (as first criteria, ascending),
        /// then by type (as second criteria, ascending) and then by price (as third criteria, ascending).
        /// </returns>
        string FindTicketsInInterval(DateTime startDateTime, DateTime endDateTime);

        int GetTicketsCount(TicketType type);
    }
}