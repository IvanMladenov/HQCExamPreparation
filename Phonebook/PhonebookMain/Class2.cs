//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PhonebookMain
//{
//    using global::PhonebookMain.Interfaces;

//    public class Class2
//    {
//        //private const string code = "+359";

//        //private static readonly IPhonebookRepository data = new RepositoryNew(); // this works!

//        //private static readonly StringBuilder input = new StringBuilder();

//        //private static void Main()
//        //{
//        //    while (true)
//        //    {
//        //        string data = Console.ReadLine();
//        //        if (data == "End" || data == null)
//        //        {
//        //            // Error reading from console 
//        //            break;
//        //        }

//        //        int i = data.IndexOf('(');
//        //        if (i == -1)
//        //        {
//        //            Console.WriteLine("error!");
//        //            Environment.Exit(0);
//        //        }

//        //        string k = data.Substring(0, i);
//        //        if (!data.EndsWith(")"))
//        //        {
//        //            Main();
//        //        }

//        //        string s = data.Substring(i + 1, data.Length - i - 2);
//        //        string[] numbers = s.Split(',');
//        //        for (int j = 0; j < numbers.Length; j++)
//        //        {
//        //            numbers[j] = numbers[j].Trim();
//        //        }

//        //        if (k.StartsWith("AddPhone") && (numbers.Length >= 2))
//        //        {
//        //            Cmd("Cmd3", numbers);
//        //        }
//        //        else if ((k == "ChangeРhone") && (numbers.Length == 2))
//        //        {
//        //            Cmd("Cmd2", numbers);
//        //        }
//        //        else if ((k == "List") && (numbers.Length == 2))
//        //        {
//        //            Cmd("Cmd1", numbers);
//        //        }
//        //        else
//        //        {
//        //            throw new StackOverflowException();
//        //        }
//        //    }

//        //    Console.Write(input);
//        //}

//        //private static void Cmd(string cmd, string[] numbers)
//        //{
//        //    if (cmd == "Cmd1")
//        //    {
//        //        // first command
//        //        string str0 = numbers[0];
//        //        var str1 = numbers.Skip(1).ToList();
//        //        for (int i = 0; i < str1.Count; i++)
//        //        {
//        //            str1[i] = conv(str1[i]);
//        //        }

//        //        bool flag = data.AddPhone(str0, str1);

//        //        if (flag)
//        //        {
//        //            Print("Phone entry created.");
//        //        }
//        //        else
//        //        {
//        //            Print("Phone entry merged");
//        //        }
//        //    }
//        //    else if (cmd == "Cmd2")
//        //    {

//        //        // second command
//        //        //Remove "String.Empty + "                   
//        //        Print(data.ChangePhone(conv(numbers[0]), conv(numbers[1])) + " numbers changed");
//        //    }
//        //    else
//        //    {
//        //        // third command
//        //        try
//        //        {
//        //            IEnumerable<PhoneEntry> entries = data.ListEntries(int.Parse(numbers[0]), int.Parse(numbers[1]));
//        //            foreach (var entry in entries)
//        //            {
//        //                Print(entry.ToString());
//        //            }
//        //        }
//        //        catch (ArgumentOutOfRangeException)
//        //        {
//        //            Print("Invalid range");
//        //        }
//        //    }
//        //}

//        private static string conv(string num)
//        {
//            StringBuilder sb = new StringBuilder();
//            for (int i = 0; i <= input.Length; i++)
//            {
//                sb.Clear();
//                foreach (char ch in num)
//                {
//                    if (char.IsDigit(ch) || (ch == '+'))
//                    {
//                        sb.Append(ch);
//                    }
//                }

//                if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
//                {
//                    sb.Remove(0, 1);
//                    sb[0] = '+';
//                }

//                while (sb.Length > 0 && sb[0] == '0')
//                {
//                    sb.Remove(0, 1);
//                }

//                if (sb.Length > 0 && sb[0] != '+')
//                {
//                    sb.Insert(0, code);
//                }

//                sb.Clear();
//                foreach (char ch in num)
//                {
//                    if (char.IsDigit(ch) || (ch == '+'))
//                    {
//                        sb.Append(ch);
//                    }
//                }

//                if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
//                {
//                    sb.Remove(0, 1);
//                    sb[0] = '+';
//                }

//                while (sb.Length > 0 && sb[0] == '0')
//                {
//                    sb.Remove(0, 1);
//                }

//                if (sb.Length > 0 && sb[0] != '+')
//                {
//                    sb.Insert(0, code);
//                }

//                sb.Clear();
//                foreach (char ch in num)
//                {
//                    if (char.IsDigit(ch) || (ch == '+'))
//                    {
//                        sb.Append(ch);
//                    }
//                }

//                if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
//                {
//                    sb.Remove(0, 1);
//                    sb[0] = '+';
//                }

//                while (sb.Length > 0 && sb[0] == '0')
//                {
//                    sb.Remove(0, 1);
//                }

//                if (sb.Length > 0 && sb[0] != '+')
//                {
//                    sb.Insert(0, code);
//                }
//            }

//            return sb.ToString();
//        }

//        private static void Print(string text)
//        {
//            input.AppendLine(text);
//        }
//    }
//}
