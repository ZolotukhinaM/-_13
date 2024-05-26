using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ЛР12_Часть4;

namespace ЛР_13
{
    public class HashTable<T> : ICollection<T>, IEnumerable<T> where T : IComparable, ICloneable
    {
        protected PointHashTable<T>[]? table;
        public PointHashTable<T>[]? Table => table;
        public int Capacity;
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        //конструктор для создания коллекции с емкостью capacity
        public HashTable(int capacity = 10)
        {
            Capacity = capacity;
            table = new PointHashTable<T>[Capacity];
            Count = 0;
        }


        //конструктор для инициализации элементами и емкостью коллекции t
        public HashTable(HashTable<T> t)
        {
            HashTable<T> tb = new HashTable<T>(t.Capacity);
            T help;
            foreach (T item in t)
            {
                help = (T)item.Clone();
                tb.Add(item);
                Count++;
            }
            table = tb.table;
            Capacity = t.Capacity;
        }


        //нумератор

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] != null)
                {
                    PointHashTable<T> p = table[i];
                    while (p != null)
                    {
                        yield return p.value;
                        p = p.next;
                    }
                }
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        //добавление элемента
        public virtual void Add(T item)
        {
            PointHashTable<T> point = new PointHashTable<T>(item);
            if (item == null) return;
            int index = point.GetHashCode() % Capacity;
            if (table[index] == null)
            {
                table[index] = point;
                Count++;
            }
            else
            {
                PointHashTable<T> cur = table[index];
                if (string.Compare(cur.key.ToString(), point.key.ToString()) == 0) return;
                while (cur.next != null)
                {
                    cur = cur.next;
                    if (string.Compare(cur.key.ToString(), point.key.ToString()) == 0) return;

                }
                cur.next = point;
                Count++;
            }
            return;
        }

        //индексатор для получения элемента по ключу
        public virtual T this[int index]
        {
            set
            {
                if (index >= 0 && index < Capacity && value != null) // проверка типа
                {
                    if (table[index] != null)
                    {
                        table[index].value = value;
                        table[index].key = value;
                    }
                    else
                    {
                        PointHashTable<T> node = new PointHashTable<T>(value);
                        table[index] = node;
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
        public void Add(params T[] mas)
        {
            foreach (T item in mas)
            {
                this.Add(item);
            }
        }

        //печать хеш-таблицы
        public void Print()
        {
            if (table == null)
            {
                Console.WriteLine("Таблица пустая!");
                return;
            }
            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] == null) Console.WriteLine(i + " : ");
                else
                {
                    Console.Write(i + " : ");
                    PointHashTable<T> p = table[i];
                    while (p != null)
                    {
                        Console.WriteLine(p);
                        p = p.next;
                    }
                    Console.WriteLine();
                }
            }
        }

        //чистка коллекции
        public void Clear()
        {
            table = new PointHashTable<T>[Capacity];
            Count = 0;
        }
        //проверка, содержит ли коллекция элемент с ключом k
        bool ICollection<T>.Contains(T k)
        {
            PointHashTable<T> point = new PointHashTable<T>(k);
            int code = Math.Abs(point.GetHashCode()) % Capacity;
            if (k.CompareTo(table[code].key) == 0)
                return true;
            point = table[code];
            while (point != null)
            {
                if (k.CompareTo(point.key) == 0) return true;
                point = point.next;
            }
            return false;
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        //копирование коллекции
        public HashTable<T> CopyFrom(HashTable<T> t)
        {
            return t;
        }
        //удаление элемента
        public virtual bool Remove(T item)
        {
            PointHashTable<T> lp = new PointHashTable<T>(item);
            int code = lp.GetHashCode() % Capacity;
            lp = table[code];
            if (table[code] == null) return false;
            if (table[code] != null && item.CompareTo(table[code].value) == 0)
            {
                table[code] = table[code].next;
                Count--;
                return true;
            }
            while (lp.next != null && (item.CompareTo(lp.next.value) != 0))
                lp = lp.next;
            if (lp.next != null)
            {
                lp.next = lp.next.next;
                Count--;
                return true;
            }
            return false;
        }

        //удаление элементов
        public bool Remove(params T[] mas)
        {
            int i = 0;
            foreach (T item in mas)
            {
                if (this.Remove(item))
                {
                    i += 1;
                }
            }
            if (i != 0)
            {
                return true;
            }
            return false;
        }
    }
}
