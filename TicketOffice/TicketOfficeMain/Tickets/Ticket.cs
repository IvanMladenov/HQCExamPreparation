namespace TicketOfficeMain.Tickets
{
    using System;
    using System.Globalization;

    public abstract class Ticket : IComparable<Ticket>
    {
        public Ticket(TicketType type, string from, string to, string date, string price)
        {
            this.Type = type;
            this.From = from;
            this.To = to;
            this.DateAndTime = ParseDateTime(date);
            if (price == null)
            {
                price = "0";
            }
            this.Price = decimal.Parse(price);
        }

        public TicketType Type { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime DateAndTime { get; set; }

        public decimal Price { get; set; }

        public abstract string DataKey { get; }

        public string TypeAsString
        {
            get
            {
                return this.Type.ToString();
            }
        }

        public string FromToKey
        {
            get
            {
                return CreateFromToKey(this.From, this.To);
            }
        }

        public int CompareTo(Ticket otherTicket)
        {
            int nateeja = this.DateAndTime.CompareTo(otherTicket.DateAndTime);
            if (nateeja == 0)
            {
                nateeja = this.Type.CompareTo(otherTicket.Type);
            }

            if (nateeja == 0)
            {
                nateeja = this.Price.CompareTo(otherTicket.Price);
            }

            return nateeja;
        }

        public override string ToString()
        {
            string input = "[" + this.DateAndTime.ToString("dd.MM.yyyy HH:mm") + "|" + this.Type.ToString().ToLower() + "|"
                           + string.Format("{0:f2}", this.Price) + "]";
            return input;
        }

        public static string CreateFromToKey(string from, string to)
        {
            return from + "; " + to;
        }

        public static DateTime ParseDateTime(string date)
        {
            if (date == null)
            {
                return default(DateTime);
            }
            DateTime result = DateTime.ParseExact(date, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            return result;
        }
    }
}