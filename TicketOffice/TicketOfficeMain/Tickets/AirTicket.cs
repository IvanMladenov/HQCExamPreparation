namespace TicketOfficeMain.Tickets
{
    public class AirTicket : Ticket
    {
        public AirTicket(string flightNumber, string from, string to, string airline, string date, string price)
            : base(TicketType.Flight, from, to, date, price)
        {
            this.FlightNumber = flightNumber;
            this.Airline = airline;
        }

        public AirTicket(string flightNumber)
            : this(flightNumber, default(string), default(string), default(string), default(string), default(string))
        {
        }

        public string Airline { get; set; }

        public string FlightNumber { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.FlightNumber;
            }
        }
    }
}