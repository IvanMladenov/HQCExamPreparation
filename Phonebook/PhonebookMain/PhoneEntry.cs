namespace PhonebookMain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PhoneEntry : IComparable<PhoneEntry>
    {
        private string caseInsensitiveName;

        private string nameWithCasing;

        //This was bottleneck by using sorted set it was performing sort after all adding
        //or changing number. Now the program sorts only when call ToString() method.
        public HashSet<string> numbers;

        public string Name
        {
            get
            {
                return this.nameWithCasing;
            }

            set
            {
                this.nameWithCasing = value;

                this.caseInsensitiveName = value.ToLowerInvariant();
            }
        }

        public int CompareTo(PhoneEntry other)
        {
            return this.caseInsensitiveName.CompareTo(other.caseInsensitiveName);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            List<string> orderd = this.numbers.ToList();
            orderd.Sort();

            sb.Append('[');
            sb.Append(this.Name);
            bool flag = true;
            foreach (var phone in orderd)
            {
                if (flag)
                {
                    sb.Append(": ");
                    flag = false;
                }
                else
                {
                    sb.Append(", ");
                }

                sb.Append(phone);
            }

            sb.Append(']');
            return sb.ToString();
        }
    }
}