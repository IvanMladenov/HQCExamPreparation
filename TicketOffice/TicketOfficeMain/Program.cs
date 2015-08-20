namespace TicketOfficeMain
{
    using System;

    public class Program
    {
        private static void Main()
        {
            TicketRepository ticket = new TicketRepository();
            while (true)
            {
                string inputLine = Console.ReadLine();
                if (inputLine == null)
                {
                    break;
                }

                inputLine = inputLine.Trim();
                string commandResult = ticket.CommandExecutor(inputLine);
                if (commandResult != null)
                {
                    Console.WriteLine(commandResult);
                }
            }
        }
    }
}