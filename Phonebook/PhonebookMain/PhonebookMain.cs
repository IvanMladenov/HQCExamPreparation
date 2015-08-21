namespace PhonebookMain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using global::PhonebookMain.Interfaces;

    internal class PhonebookMain
    {
        private const string code = "+359";

        private static readonly IPhonebookRepository data = new PhonebookRepository(); // this works!

        private static readonly StringBuilder output = new StringBuilder();

        private static void Main()
        {
            while (true)
            {
                string data = Console.ReadLine();
                if (data == "End" || data == null)
                {
                    // Error reading from console 
                    break;
                }

                int i = data.IndexOf('(');
                if (i == -1)
                {
                    Console.WriteLine("error!");
                    Environment.Exit(0);
                }

                string command = data.Substring(0, i);

                string parametersLine = data.Substring(i + 1, data.Length - i - 2);
                string[] strings = parametersLine.Split(',');
                for (int j = 0; j < strings.Length; j++)
                {
                    strings[j] = strings[j].Trim();
                }

                if (command.StartsWith("AddPhone") && (strings.Length >= 2))
                {
                    AppendAddPhoneResults(strings);
                }
                else if ((command == "ChangePhone") && (strings.Length == 2))
                {
                    AppendChangePhoneResult(strings);
                }
                else if ((command == "List") && (strings.Length == 2))
                {
                    AppendListResult(strings);
                }
                else
                {
                    throw new ArgumentException("Invalid command!");
                }
            }

            Console.Write(output);
        }

        private static void AppendListResult(string[] strings)
        {           
                try
                {
                    IEnumerable<PhoneEntry> entries = data.ListEntries(int.Parse(strings[0]), int.Parse(strings[1]));
                    foreach (var entry in entries)
                    {
                        OutputAppend(entry.ToString());
                    }
                }
                catch (NullReferenceException)
                {
                    OutputAppend("Invalid range");
                }           
        }

        private static void AppendChangePhoneResult(string[] strings)
        {
            OutputAppend(data.ChangePhone(ConvertToCanonical(strings[0]), ConvertToCanonical(strings[1])) + " numbers changed");
        }

        private static void AppendAddPhoneResults(string[] strings)
        {
            string str0 = strings[0];
            var str1 = strings.Skip(1).ToList();
            for (int i = 0; i < str1.Count; i++)
            {
                str1[i] = ConvertToCanonical(str1[i]);
            }

            bool flag = data.AddPhone(str0, str1);

            if (flag)
            {
                OutputAppend("Phone entry created");
            }
            else
            {
                OutputAppend("Phone entry merged");
            }
        }

        private static string ConvertToCanonical(string number)
        {
            StringBuilder sb = new StringBuilder();
            
                foreach (char ch in number)
                {
                    if (char.IsDigit(ch) || (ch == '+'))
                    {
                        sb.Append(ch);
                    }
                }

                if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
                {
                    sb.Remove(0, 1);
                    sb[0] = '+';
                }

                while (sb.Length > 0 && sb[0] == '0')
                {
                    sb.Remove(0, 1);
                }

                if (sb.Length > 0 && sb[0] != '+')
                {
                    sb.Insert(0, code);
                }

            return sb.ToString();
        }

        private static void OutputAppend(string text)
        {
            output.AppendLine(text);
        }
    }
}