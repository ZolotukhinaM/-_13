using System;
using ClassLibrary2;
using ЛР_13;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NewHashtable()
        {
            NewHashTable<CelestialBody> table1 = new NewHashTable<CelestialBody>(10, "Коллекция 1");
            NewHashTable<CelestialBody> table2 = new NewHashTable<CelestialBody>(10, "Коллекция 1");
            Assert.AreEqual(table1, table2);
            table1[0] = new CelestialBody("Нептун", 100, 30);
            for (int i = 0; i < 10; i++)
            {
                CelestialBody p = new CelestialBody();
                p.RandomInit();
                table2.Add(p);
            }

        }
        [TestMethod]
        public void PointHashTable()
        {
            CelestialBody p = new CelestialBody();
            PointHashTable<CelestialBody> table1 = new PointHashTable<CelestialBody>(p);
            PointHashTable<CelestialBody> table2 = new PointHashTable<CelestialBody>(p);
            Console.WriteLine(table2);
            Assert.AreEqual(table1, table2); // этот тест теперь будет сравнивать содержимое объектов
        }

        [TestMethod]
        public void Journal()
        {
            NewHashTable<CelestialBody> table1 = new NewHashTable<CelestialBody>(10, "Коллекция 1");
            Journal j1 = new Journal();
            Journal j2 = new Journal();
            table1.CollectionReferenceChanged += j1.Writer;
            table1.CollectionReferenceChanged += j2.Writer;
            Assert.AreEqual(j1, j2);
            table1.Clear();
        }
        [TestMethod]
        public void JournalEntry()
        {
            NewHashTable<CelestialBody> table1 = new NewHashTable<CelestialBody>(10, "Коллекция 1");
            JournalEntry j1 = new JournalEntry("", "", "");
            JournalEntry j2 = new JournalEntry("", "", "");
            Assert.AreEqual(j1, j2);
        }
        [TestMethod]
        public void Add()
        {
            // Arrange
            var hashTable = new HashTable<CelestialBody>(10);
            var body = new CelestialBody("Sun", 1, 1);

            // Act
            hashTable.Add(body);

            // Assert
            Assert.IsTrue(((ICollection<CelestialBody>)hashTable).Contains(body), "The added CelestialBody object was not found in the hash table.");
        }
        [TestMethod]
        public void Remove()
        {
            // Arrange
            var hashTable = new HashTable<CelestialBody>(10);
            var body = new CelestialBody("Sun", 1, 1);

            // Act
            bool result = hashTable.Remove(body);

            // Assert
            Assert.IsFalse(result, "Expected false when attempting to remove non-existent CelestialBody object.");
        }
        [TestMethod]
        public void Add2()
        {
            var hashTable = new HashTable<CelestialBody>(10);

            // Arrange
            var earth = new CelestialBody("Earth", 100, 50);
            var mars = new CelestialBody("Mars", 200, 75);

            // Act
            hashTable.Add(earth, mars);

            // Assert
            Assert.IsTrue(hashTable.Contains(earth), "The Earth item is not in the hash table.");
            Assert.IsTrue(hashTable.Contains(mars), "The Mars item is not in the hash table.");
        }
        [TestMethod]
        public void JournalEntry_ToString()
        {
            // Arrange
            var journalEntry = new JournalEntry("MyCollection", "Add", "Item1");

            // Act
            var result = journalEntry.ToString();

            // Assert
            Assert.AreEqual("В коллекции MyCollection произошло событие Add. Элемент Item1 ", result);
        }
        [TestMethod]
        public void JournalEntry1()
        {
            // Arrange
            var journalEntry1 = new JournalEntry("MyCollection", "Add", "Item1");
            var journalEntry2 = new JournalEntry("MyCollection", "Add", "Item1");

            // Act & Assert
            Assert.AreEqual(journalEntry1, journalEntry2);
        }

        [TestMethod]
        public void JournalEntry2()
        {
            // Arrange
            var journalEntry = new JournalEntry("MyCollection", "Add", "Item1");

            // Act & Assert
            Assert.IsFalse(journalEntry.Equals("Some String"));
        }

        [TestMethod]
        public void Constructor_InitializesCapacityAndTable()
        {
            // Arrange
            int capacity = 10;

            // Act
            var hashTable = new HashTable<CelestialBody>(capacity);

            // Assert
            Assert.AreEqual(capacity, hashTable.Capacity);
            Assert.AreEqual(capacity, hashTable.Table.Length);
            Assert.AreEqual(0, hashTable.Count);
        }
        [TestMethod]
        public void Constructor_CloneAnotherHashTable()
        {
            // Arrange
            var originalTable = new HashTable<CelestialBody>(10);
            originalTable.Add(new CelestialBody("Cjkywt", 22,34));
            originalTable.Add(new CelestialBody("1234", 22, 34));

            // Act
            var clonedTable = new HashTable<CelestialBody>(originalTable);

            // Assert
            Assert.AreEqual(originalTable.Capacity, clonedTable.Capacity);
            Assert.AreEqual(originalTable.Count, clonedTable.Count);
            var origEnumerator = originalTable.GetEnumerator();
            var cloneEnumerator = clonedTable.GetEnumerator();
            while (origEnumerator.MoveNext() && cloneEnumerator.MoveNext())
            {
                Assert.AreEqual(origEnumerator.Current, cloneEnumerator.Current);
            }
        }
        [TestMethod]
        public void Enumerator_ReturnsAllValues()
        {
            // Arrange
            var hashTable = new HashTable<CelestialBody>(10);
            var items = new List<CelestialBody>
        {
            new CelestialBody("Cjkywt", 22,34),
            new CelestialBody("123", 22,34),
            new CelestialBody("12345", 22,34)
        };

            foreach (var item in items)
            {
                hashTable.Add(item);
            }

            // Act
            var enumerator = hashTable.GetEnumerator();

            // Assert
            var result = new List<CelestialBody>();
            while (enumerator.MoveNext())
            {
                result.Add(enumerator.Current);
            }

            CollectionAssert.AreEquivalent(items, result);
        }
    }
}