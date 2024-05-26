using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ЛР_13
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string TypeChange { get; set; }
        public string Data { get; set; }
        public JournalEntry(string name, string change, string data)
        {
            CollectionName = name;
            TypeChange = change;
            Data = data;
        }
        public override string ToString()
        {
            return $"В коллекции {CollectionName} произошло событие {TypeChange}. Элемент {Data} ";
        }
        public override bool Equals(object? obj)
        {
            if (obj is JournalEntry p)
            {
                return this.CollectionName == CollectionName && this.TypeChange == p.TypeChange && this.Data == p.Data;
            }
            else { return false; }
        }
    }
}
