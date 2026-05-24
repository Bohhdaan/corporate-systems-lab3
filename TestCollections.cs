using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

public class TestCollections
{
    // Standard collections
    private List<Person> personList;
    private List<string> stringList;
    private Dictionary<Person, Student> personDict;
    private Dictionary<string, Student> stringDict;

    // Immutable collections
    private ImmutableList<Person> immutablePersonList;
    private ImmutableList<string> immutableStringList;
    private ImmutableDictionary<Person, Student> immutablePersonDict;
    private ImmutableDictionary<string, Student> immutableStringDict;

    // Sorted collections
    private SortedList<Person, Student> sortedPersonList;
    private SortedList<string, Student> sortedStringList;
    private SortedDictionary<Person, Student> sortedPersonDict;
    private SortedDictionary<string, Student> sortedStringDict;

    public static Student GenerateStudent(int index)
    {
        Person p = new Person(
            $"Name{index}",
            $"Surname{index}",
            new DateTime(2000 + (index % 10), (index % 12) + 1, (index % 28) + 1)
        );

        Student s = new Student(p, Education.Bachelor, 101 + (index % 599));

        s.AddExams(
            new Exam("Math", (index % 5) + 1, new DateTime(2025, 5, 10)),
            new Exam("Programming", ((index + 2) % 5) + 1, new DateTime(2025, 5, 12))
        );

        s.AddTests(
            new Test("Algorithms", index % 2 == 0)
        );

        return s;
    }

    public TestCollections(int count)
    {
        personList = new List<Person>();
        stringList = new List<string>();
        personDict = new Dictionary<Person, Student>();
        stringDict = new Dictionary<string, Student>();

        // SortedList/SortedDictionary потребують компаратора для Person
        PersonComparer personComparer = new PersonComparer();
        sortedPersonList = new SortedList<Person, Student>(personComparer);
        sortedStringList = new SortedList<string, Student>();
        sortedPersonDict = new SortedDictionary<Person, Student>(personComparer);
        sortedStringDict = new SortedDictionary<string, Student>();

        for (int i = 0; i < count; i++)
        {
            Student student = GenerateStudent(i);
            Person key = student.Person;
            string keyStr = key.ToString();

            personList.Add(key);
            stringList.Add(keyStr);
            personDict[key] = student;
            stringDict[keyStr] = student;

            // SortedList і SortedDictionary не допускають дублікатів ключів
            if (!sortedPersonList.ContainsKey(key))
                sortedPersonList.Add(key, student);
            if (!sortedStringList.ContainsKey(keyStr))
                sortedStringList.Add(keyStr, student);
            if (!sortedPersonDict.ContainsKey(key))
                sortedPersonDict.Add(key, student);
            if (!sortedStringDict.ContainsKey(keyStr))
                sortedStringDict.Add(keyStr, student);
        }

        // Immutable будуються з готових стандартних колекцій
        immutablePersonList = ImmutableList.CreateRange(personList);
        immutableStringList = ImmutableList.CreateRange(stringList);
        immutablePersonDict = ImmutableDictionary.CreateRange(personDict);
        immutableStringDict = ImmutableDictionary.CreateRange(stringDict);
    }

    public void MeasureSearchTime(int count)
    {
        int[] indices = new int[] { 0, count / 2, count - 1, -1 };
        string[] labels = new string[] { "First", "Middle", "Last", "Not in collection" };

        Stopwatch sw = new Stopwatch();

        for (int i = 0; i < indices.Length; i++)
        {
            Student searchTarget;
            Person personKey;
            string stringKey;

            if (indices[i] == -1)
            {
                searchTarget = GenerateStudent(count + 999);
                personKey = searchTarget.Person;
                stringKey = personKey.ToString();
            }
            else
            {
                searchTarget = GenerateStudent(indices[i]);
                personKey = searchTarget.Person;
                stringKey = personKey.ToString();
            }

            Console.WriteLine($"--- {labels[i]} element ---");

            // === STANDARD ===
            sw.Restart();
            bool r1 = personList.Contains(personKey);
            sw.Stop();
            Console.WriteLine($"[Standard]  List<Person>.Contains:                    {sw.ElapsedTicks,8} ticks, found={r1}");

            sw.Restart();
            bool r2 = stringList.Contains(stringKey);
            sw.Stop();
            Console.WriteLine($"[Standard]  List<string>.Contains:                    {sw.ElapsedTicks,8} ticks, found={r2}");

            sw.Restart();
            bool r3 = personDict.ContainsKey(personKey);
            sw.Stop();
            Console.WriteLine($"[Standard]  Dictionary<Person,Student>.ContainsKey:   {sw.ElapsedTicks,8} ticks, found={r3}");

            sw.Restart();
            bool r4 = stringDict.ContainsKey(stringKey);
            sw.Stop();
            Console.WriteLine($"[Standard]  Dictionary<string,Student>.ContainsKey:   {sw.ElapsedTicks,8} ticks, found={r4}");

            sw.Restart();
            bool r5 = personDict.ContainsValue(searchTarget);
            sw.Stop();
            Console.WriteLine($"[Standard]  Dictionary<Person,Student>.ContainsValue: {sw.ElapsedTicks,8} ticks, found={r5}");

            // === IMMUTABLE ===
            sw.Restart();
            bool r6 = immutablePersonList.Contains(personKey);
            sw.Stop();
            Console.WriteLine($"[Immutable] ImmutableList<Person>.Contains:           {sw.ElapsedTicks,8} ticks, found={r6}");

            sw.Restart();
            bool r7 = immutableStringList.Contains(stringKey);
            sw.Stop();
            Console.WriteLine($"[Immutable] ImmutableList<string>.Contains:           {sw.ElapsedTicks,8} ticks, found={r7}");

            sw.Restart();
            bool r8 = immutablePersonDict.ContainsKey(personKey);
            sw.Stop();
            Console.WriteLine($"[Immutable] ImmutableDictionary<Person>.ContainsKey:  {sw.ElapsedTicks,8} ticks, found={r8}");

            sw.Restart();
            bool r9 = immutableStringDict.ContainsKey(stringKey);
            sw.Stop();
            Console.WriteLine($"[Immutable] ImmutableDictionary<string>.ContainsKey:  {sw.ElapsedTicks,8} ticks, found={r9}");

            // === SORTED ===
            sw.Restart();
            bool r10 = sortedPersonList.ContainsKey(personKey);
            sw.Stop();
            Console.WriteLine($"[Sorted]    SortedList<Person,Student>.ContainsKey:   {sw.ElapsedTicks,8} ticks, found={r10}");

            sw.Restart();
            bool r11 = sortedStringList.ContainsKey(stringKey);
            sw.Stop();
            Console.WriteLine($"[Sorted]    SortedList<string,Student>.ContainsKey:   {sw.ElapsedTicks,8} ticks, found={r11}");

            sw.Restart();
            bool r12 = sortedPersonDict.ContainsKey(personKey);
            sw.Stop();
            Console.WriteLine($"[Sorted]    SortedDictionary<Person,Student>.ContainsKey: {sw.ElapsedTicks,8} ticks, found={r12}");

            sw.Restart();
            bool r13 = sortedStringDict.ContainsKey(stringKey);
            sw.Stop();
            Console.WriteLine($"[Sorted]    SortedDictionary<string,Student>.ContainsKey: {sw.ElapsedTicks,8} ticks, found={r13}");

            Console.WriteLine();
        }
    }

    // компаратор для Person як ключа в Sorted-колекціях
    private class PersonComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            int cmp = string.Compare(x.Surname, y.Surname, StringComparison.Ordinal);
            if (cmp != 0) return cmp;
            cmp = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            if (cmp != 0) return cmp;
            return DateTime.Compare(x.BirthDate, y.BirthDate);
        }
    }
}
