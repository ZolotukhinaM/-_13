using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР_13
{
    public class Journal
    {
        public List<JournalEntry> entries = new List<JournalEntry>();
        public void Writer(object source, CollectionHandlerEventArgs args)
        {
            JournalEntry entry = new JournalEntry(((NewHashTable<CelestialBody>)source).Name, args.CollectionCountChanged, args.CollectionReferenceChanged);
            entries.Add(entry);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Journal other = (Journal)obj;

            // Сравнивайте все значения, которые делают ваши объекты Journal равными по содержимому
            return this.entries.SequenceEqual(other.entries); // пример сравнения, если entries совместимы для сравнения
        }

        public override int GetHashCode()
        {
            // Определите хеш-код на основе свойств, которые вы сравниваете в Equals
            return this.entries.Aggregate(0, (hash, entry) => hash ^ entry.GetHashCode());
        }
    }
}
