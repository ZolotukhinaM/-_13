using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР_13
{
    public class CollectionHandlerEventArgs
    {
        public string? CollectionCountChanged { get; set; }
        public string? CollectionReferenceChanged { get; set; }
        public CollectionHandlerEventArgs(string countChanged, object? referenceChanged)
        {
            CollectionCountChanged = countChanged;
            CollectionReferenceChanged = referenceChanged?.ToString();
        }
    }
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
}
