namespace PhonebookMain.Interfaces
{
    using System.Collections.Generic;

    public interface IPhonebookRepository
    {
        /// <summary>
        /// Create new entry if the name is not existing in database or merge the phones of the existing name.
        /// </summary>
        /// <param name="name">Name of the entry which is unique.</param>
        /// <param name="phoneNumbers">Collection of phone numbers which have to be added to the name</param>
        /// <returns>Returns true if the name does not exist in database and false if the name exist.</returns>
        bool AddPhone(string name, IEnumerable<string> phoneNumbers);

        /// <summary>
        /// Change existing phone number provided with new one also in paramethers.
        /// </summary>
        /// <param name="oldPhoneNumber">The number that have to be changed.</param>
        /// <param name="newPhoneNumber">Provide the new number, replace old number.</param>
        /// <returns>Returns number of phone numbers changed.</returns>
        int ChangePhone(string oldPhoneNumber, string newPhoneNumber);

        /// <summary>
        /// Returns ordered collection of entries , starting from specified index to given count. In case 
        /// of invalid start index or invalid count print message "Invalid Range"
        /// </summary>
        /// <param name="startIndex">Define start index of collection that have to be printed.</param>
        /// <param name="count">Define number of the entries tha have to be added to output collection</param>
        /// <returns>Return collection of ordered entries.</returns>
        PhoneEntry[] ListEntries(int startIndex, int count);
    }
}