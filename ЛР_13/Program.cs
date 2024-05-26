using ClassLibrary2;
using System;
using ЛР_13;
using ЛР12_Часть4;

internal class Program
{
    static void Main(string[] args)
    {
        NewHashTable<CelestialBody> table1 = new NewHashTable<CelestialBody>(10, "Коллекция 1");
        NewHashTable<CelestialBody> table2 = new NewHashTable<CelestialBody>(10, "Коллекция 2");
        Journal j1 = new Journal();
        Journal j2 = new Journal();
       
        // Подписываем журналы на события коллекций для table1
        table1.CollectionReferenceChanged += j1.Writer;
        table1.CollectionCountChanged += j1.Writer;
        table1.CollectionReferenceChanged += j2.Writer;
        table1.CollectionCountChanged += j2.Writer;

        // Подписываем журналы на события коллекций для table2
        table2.CollectionReferenceChanged += j2.Writer;
        table2.CollectionCountChanged += j2.Writer;

        for (int i = 0; i < 10; i++)
        {
            CelestialBody p = new CelestialBody();
            p.RandomInit();
            table1.Add(p);
        }
        table1[0] = new CelestialBody("Нептун", 100, 30);
      

        for (int i = 0; i < 10; i++)
        {
            CelestialBody p = new CelestialBody();
            p.RandomInit();
            table2.Add(p);
        }
        table2[1] = new CelestialBody("Венера", 200, 60);


        for (int i = 0; i < 3; i++)
        {
            CelestialBody p = new CelestialBody($"Планета_{i}", i * 10, i * 5);
            table1.Remove(p);
        }

        for (int i = 0; i < 3; i++)
        {
            CelestialBody p = new CelestialBody($"Планета_{i}", i * 10, i * 5);
            table2.Remove(p);
        }

        foreach (var item in j2.entries)
        {
            Console.WriteLine(item);
        }
    }

}
