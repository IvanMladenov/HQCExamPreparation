namespace PhonebookMain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::PhonebookMain.Interfaces;

    using Wintellect.PowerCollections;

    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly Dictionary<string, PhoneEntry> caseInsensitiveKeyName = new Dictionary<string, PhoneEntry>();

        private readonly MultiDictionary<string, PhoneEntry> keyNumberValueEntry = new MultiDictionary<string, PhoneEntry>(false);

        private readonly OrderedSet<PhoneEntry> phoneEntrySorted = new OrderedSet<PhoneEntry>();

        public int PhoneEntrySortedCount
        {
            get
            {
                return this.phoneEntrySorted.Count;
            }
        }

        public int KeyNumberValueEntryCount
        {
            get
            {
                return this.keyNumberValueEntry.Count;
            }
        }

        public bool AddPhone(string name, IEnumerable<string> phoneNumbers)
        {
            string caseInsensitiveName = name.ToLowerInvariant();
            PhoneEntry entry;
            bool isNotExistingNumber = !this.caseInsensitiveKeyName.TryGetValue(caseInsensitiveName, out entry);
            if (isNotExistingNumber)
            {
                entry = new PhoneEntry();
                entry.Name = name;
                entry.numbers = new HashSet<string>();
                this.caseInsensitiveKeyName.Add(caseInsensitiveName, entry);

                this.phoneEntrySorted.Add(entry);
            }

            foreach (var number in phoneNumbers)
            {
                this.keyNumberValueEntry.Add(number, entry);
            }

            entry.numbers.UnionWith(phoneNumbers);
            return isNotExistingNumber;
        }

        public int ChangePhone(string oldNumber, string newNumber)
        {
            var found = this.keyNumberValueEntry[oldNumber].ToList();
            
            foreach (var entry in found)
            {
                entry.numbers.Remove(oldNumber);
                this.keyNumberValueEntry.Remove(oldNumber, entry);

                entry.numbers.Add(newNumber);
                this.keyNumberValueEntry.Add(newNumber, entry);
            }

            return found.Count;
        }

        public PhoneEntry[] ListEntries(int startPositon, int count)
        {
            if (startPositon < 0 || startPositon + count > this.caseInsensitiveKeyName.Count)
            {
                return null;
            }

            PhoneEntry[] list = new PhoneEntry[count];
            for (int i = startPositon; i <= startPositon + count - 1; i++)
            {
                PhoneEntry entry = this.phoneEntrySorted[i];
                list[i - startPositon] = entry;
            }

            return list;
        }
    }
}