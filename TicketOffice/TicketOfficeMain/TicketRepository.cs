namespace TicketOfficeMain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using TicketOfficeMain.Inerfaces;
    using TicketOfficeMain.Tickets;

    using Wintellect.PowerCollections;

    public class TicketRepository : ITicketRepository
    {
        private readonly MultiDictionary<string, Ticket> ticketsByFromToKey = new MultiDictionary<string, Ticket>(true);

        private readonly Dictionary<string, Ticket> ticketsByDataKey = new Dictionary<string, Ticket>();

        private readonly OrderedMultiDictionary<DateTime, Ticket> ticketsByDateAndTime =
            new OrderedMultiDictionary<DateTime, Ticket>(true);

        private int airTicketsCount;

        private int busTicketsCount;

        private int trainTicketsCount;

        public string FindTickets(string from, string to)
        {
            string fromToKey = Ticket.CreateFromToKey(from, to);
            if (this.ticketsByFromToKey.ContainsKey(fromToKey))
            {
                List<Ticket> ticketsFound = this.ticketsByFromToKey[fromToKey].ToList();
                string ticketsAsString = ReadTickets(ticketsFound);
                return ticketsAsString;
            }

            return GlobalConstants.NoMatches;
        }

        public string FindTicketsInInterval(DateTime startDateTime, DateTime endDateTime)
        {
            var ticketsFound = this.ticketsByDateAndTime.Range(startDateTime, true, endDateTime, true).Values;
            if (ticketsFound.Count > 0)
            {
                return string.Join(" ", ticketsFound);
            }

            return GlobalConstants.NoMatches;
        }

        public string AddAirTicket(string flightNumber, string from, string to, string airline, DateTime dateTime, decimal price)
        {
            return this.AddAirTicket(flightNumber, from, to, airline, dateTime.ToString(GlobalConstants.DateTimeFormat), price.ToString(CultureInfo.InvariantCulture));
        }

        string ITicketRepository.DeleteAirTicket(string flightNumber)
        {
            return this.DeleteAirTicket(flightNumber);
        }

        public string AddTrainTicket(string from, string to, DateTime dateTime, decimal price, decimal studentPrice)
        {
            return this.AddTrainTicket(
                from, 
                to, 
                dateTime.ToString(GlobalConstants.DateTimeFormat), 
                price.ToString(CultureInfo.InvariantCulture), 
                studentPrice.ToString(CultureInfo.InvariantCulture));
        }

        public string DeleteTrainTicket(string from, string to, DateTime dateTime)
        {
            return this.DeleteTrainTicket(from, to, dateTime.ToString(GlobalConstants.DateTimeFormat));
        }

        public string AddBusTicket(string from, string to, string travelCompany, DateTime dateTime, decimal price)
        {
            return this.AddBusTicket(
                from, 
                to, 
                travelCompany, 
                dateTime.ToString(GlobalConstants.DateTimeFormat), 
                price.ToString());
        }

        public string DeleteBusTicket(string from, string to, string travelCompany, DateTime dateTime)
        {
            return this.DeleteBusTicket(from, to, travelCompany, dateTime.ToString(GlobalConstants.DateTimeFormat));
        }

        public int GetTicketsCount(TicketType type)
        {
            if (type == TicketType.Flight)
            {
                return this.airTicketsCount;
            }

            if (type == TicketType.Bus)
            {
                return this.busTicketsCount;
            }

            return this.trainTicketsCount;
        }

        internal string AddTicket(Ticket ticket)
        {          
                string key = ticket.DataKey;
                if (this.ticketsByDataKey.ContainsKey(key))
                {
                    return GlobalConstants.Duplicated + ticket.Type.ToString().ToLower();
                }

                this.ticketsByDataKey.Add(key, ticket);
                string fromToKey = ticket.FromToKey;

                this.ticketsByFromToKey.Add(fromToKey, ticket);
                this.ticketsByDateAndTime.Add(ticket.DateAndTime, ticket);

                return ticket.Type + " " + GlobalConstants.Created;                        
        }

        internal string DeleteTicket(Ticket ticket)
        {           
            string key = ticket.DataKey;
            if (this.ticketsByDataKey.ContainsKey(key))
            {
                ticket = this.ticketsByDataKey[key];
                this.ticketsByDataKey.Remove(key);
                string fromToKey = ticket.FromToKey;

                this.ticketsByFromToKey.Remove(fromToKey, ticket);
                this.ticketsByDateAndTime.Remove(ticket.DateAndTime, ticket);
                return ticket.Type + " " + GlobalConstants.Deleted;
            }

            return ticket.Type + " does not exist";            
        }

        public string AddAirTicket(string flightNumber, string from, string to, string airline, string dateAndTime, string price)
        {
            AirTicket ticket = new AirTicket(flightNumber, from, to, airline, dateAndTime, price);

            string result = this.AddTicket(ticket);
            if (result.Contains(GlobalConstants.Created))
            {
                this.airTicketsCount++;
            }

            return result;
        }

        protected string DeleteAirTicket(string flightNumber)
        {
            AirTicket ticket = new AirTicket(flightNumber);

            string result = this.DeleteTicket(ticket);
            if (result.Contains(GlobalConstants.Deleted))
            {
                this.airTicketsCount--;
            }

            return result;
        }

        public string AddTrainTicket(string from, string to, string dateAndTime, string price, string studentPrice)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateAndTime, price, studentPrice);

            string result = this.AddTicket(ticket);
            if (result.Contains(GlobalConstants.Created))
            {
                this.trainTicketsCount++;
            }

            return result;
        }

        private string DeleteTrainTicket(string from, string to, string dateAndTime)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateAndTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains(GlobalConstants.Deleted))
            {
                this.trainTicketsCount--;
            }

            return result;
        }

        protected string AddBusTicket(string from, string to, string travelCompany, string dateTime, string price)
        {
            BusTicket ticket = new BusTicket(from, to, travelCompany, dateTime, price);
            string key = ticket.DataKey;
            string result;

            if (this.ticketsByDataKey.ContainsKey(key))
            {
                result = GlobalConstants.Duplicated + ticket.Type.ToString().ToLower();
            }
            else
            {
                this.ticketsByDataKey.Add(key, ticket);
                string fromToKey = ticket.FromToKey;

                this.ticketsByFromToKey.Add(fromToKey, ticket);
                this.ticketsByDateAndTime.Add(ticket.DateAndTime, ticket);
                result = ticket.Type + " " + GlobalConstants.Created;
                this.busTicketsCount++;
            }

            return result;
        }

        private string DeleteBusTicket(string from, string to, string travelCompany, string dateAndTime)
        {
            BusTicket ticket = new BusTicket(from, to, travelCompany, dateAndTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains(GlobalConstants.Deleted))
            {
                this.busTicketsCount--;
            }

            return result;
        }

        internal static string ReadTickets(ICollection<Ticket> tickets)
        {
            List<Ticket> sortedTickets = new List<Ticket>(tickets);

            sortedTickets.Sort();
            string result = string.Empty;

            for (int i = 0; i < sortedTickets.Count; i++)
            {
                Ticket ticket = sortedTickets[i];

                result += ticket.ToString();

                if (i < sortedTickets.Count - 1)
                {
                    result += " ";
                }
            }

            return result;
        }

        public string findTicketsInInterval(string startDateTimeStr, string endDateTimeStr)
        {
            DateTime startDateTime = Ticket.ParseDateTime(startDateTimeStr);

            DateTime endDateTime = Ticket.ParseDateTime(endDateTimeStr);

            string ticketsAsString = this.FindTicketsInInterval(startDateTime, endDateTime);
            return ticketsAsString;
        }

        public string FindTicketsInInterval2(DateTime startDateTime, DateTime endDateTime)
        {
            var ticketsFound =
                this.ticketsByDateAndTime.Values.Where(t => t.DateAndTime >= startDateTime)
                    .Where(t => t.DateAndTime <= endDateTime)
                    .ToList();

            if (ticketsFound.Count > 0)
            {
                string ticketsAsString = ReadTickets(ticketsFound);
                return ticketsAsString;
            }

            return GlobalConstants.NoMatches;
        }

        internal string CommandExecutor(string line)
        {
            int firstSpaceIndex = line.IndexOf(' ');
            string command = this.CommandExtractor(line, firstSpaceIndex);
            string allParameters = line.Substring(firstSpaceIndex + 1);
            string[] parameters = this.GetParametersAsArray(allParameters);

            switch (command)
            {
                case "CreateFlight":
                    command = this.AddAirTicket(
                        parameters[0], 
                        parameters[1], 
                        parameters[2], 
                        parameters[3], 
                        parameters[4], 
                        parameters[5]);
                    break;

                case "DeleteFlight":
                    command = this.DeleteAirTicket(parameters[0]);
                    break;

                case "CreateTrain":
                    command = this.AddTrainTicket(
                        parameters[0], 
                        parameters[1], 
                        parameters[2], 
                        parameters[3], 
                        parameters[4]);
                    break;

                case "DeleteTrain":
                    command = this.DeleteTrainTicket(parameters[0], parameters[1], parameters[2]);
                    break;

                case "CreateBus":
                    command = this.AddBusTicket(
                        parameters[0], 
                        parameters[1], 
                        parameters[2], 
                        parameters[3], 
                        parameters[4]);
                    break;

                case "DeleteBus":
                    command = this.DeleteBusTicket(parameters[0], parameters[1], parameters[2], parameters[3]);
                    break;

                case "FindTickets":
                    command = this.FindTickets(parameters[0], parameters[1]);
                    break;

                case "FindByDates":
                    command = this.findTicketsInInterval(parameters[0], parameters[1]);
                    break;
            }

            return command;
        }

        private string[] GetParametersAsArray(string allParameters)
        {
            string[] parameters = allParameters.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = parameters[i].Trim();
            }

            return parameters;
        }

        private string CommandExtractor(string line, int firstSpaceIndex)
        {
            if (line == string.Empty)
            {
                return null;
            }

            if (firstSpaceIndex == -1)
            {
                return GlobalConstants.InvalidCommand;
            }

            return line.Substring(0, firstSpaceIndex);
        }
    }
}