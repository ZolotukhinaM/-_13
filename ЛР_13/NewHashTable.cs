using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary2;
using ЛР12_Часть4;

namespace ЛР_13
{

    public class NewHashTable<T> : HashTable<T> where T : IComparable, ICloneable
    {
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;
        public string Name { get; set; }
        public NewHashTable(int capacity, string name) : base(capacity)
        {
            Name = name;
        }
        public void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionCountChanged?.Invoke(source, args);
        }
        public void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(source, args);
        }
        public override void Add(T item)
        {
            base.Add(item);

            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Добавили новый элемент", item));
        }
       
        public override bool Remove(T item)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs("Удаляем элемент", item));
            return base.Remove(item);

        }
        public override T this[int index]
        {
            set
            {
                if (index >= 0 && index < Capacity && value != null) // проверка типа
                {
                    if (table[index] != null)
                    {
                        table[index].value = value;
                        table[index].key = value;
                        OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs("Изменен элемент", value));
                    }
                    else
                    {
                        PointHashTable<T> node = new PointHashTable<T>(value);
                        table[index] = node;
                        OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs("Изменен элемент", value));
                    }
                }
                else
                {
                    if (value == null)
                        throw new ArgumentNullException("Значение не может быть равно null");
                    if (index < 0 || index >= Capacity)
                        throw new IndexOutOfRangeException("Вы вышли за пределы диапазона");
                }
            }

        }
        public override bool Equals(object? obj)
        {
            if (obj is NewHashTable<T> p)
            {
                return this.Name == p.Name;
            }
            else { return false; }
        }
    }

}
