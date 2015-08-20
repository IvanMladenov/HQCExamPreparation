namespace TicketOfficeMain.Tickets
{
    public class BusTicket : Ticket
    {
        public BusTicket(string from, string to, string company, string date, string price)
            : base(TicketType.Bus, from, to, date, price)
        {
            this.Company = company;
        }

        public BusTicket(string from, string to, string company, string date)
            : this(from, to, company, date, default(string))
        {
        }

        public string Company { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.From + ";" + this.To + ";" + this.Company + this.DateAndTime + ";";
            }
        }
    }
}