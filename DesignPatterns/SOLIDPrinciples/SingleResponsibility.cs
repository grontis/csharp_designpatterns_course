using System;
using System.Collections.Generic;
using System.IO;

namespace SOLIDPrinciples
{
    /// Single Responsibility Principle example using a journal.
    /// Here the journal has methods of just handling the journal's responsibilities.
    /// Below is another class for maintaining journal persistence via file reading and writing,
    /// which allows for the single responsibility principle to be followed

    
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count;
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    // To keep with single responsibility principle, this class handles persistence methods for the journal
    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists((filename)))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }
}