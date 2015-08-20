namespace TicketOfficeMain.Tickets
{
    public class TrainTicket : Ticket
    {
        public TrainTicket(string from, string to, string date, string price, string studentPrice)
            : base(TicketType.Train, from, to, date, price)
        {
            if (studentPrice == null)
            {
                studentPrice = "0";
            }

            this.StudentPrice = decimal.Parse(studentPrice);
        }

        public TrainTicket(string from, string to, string date)
            : this(from, to, date, default(string), default(string))
        {
        }

        public decimal StudentPrice { get; set; }

        public override string DataKey
        {
            get
            {
                return this.Type + ";;" + this.From + ";" + this.To + ";" + this.DateAndTime + ";";
            }
        }
    }
}