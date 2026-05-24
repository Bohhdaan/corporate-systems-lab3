using System;
using System.Collections.Generic;
using System.Diagnostics;

public class TestCollections
{
    private List<Person> personList;
    private List<string> stringList;
    private Dictionary<Person, Student> personDict;
    private Dictionary<string, Student> stringDict;

    // генерація студента за цілочисельним індексом
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

    // конструктор - заповнює всі 4 колекції
    public TestCollections(int count)
    {
        personList = new List<Person>();
        stringList = new List<string>();
        personDict = new Dictionary<Person, Student>();
        stringDict = new Dictionary<string, Student>();

        for (int i = 0; i < count; i++)
        {
            Student student = GenerateStudent(i);
            Person key = student.Person;
            string keyStr = key.ToString();

            personList.Add(key);
            stringList.Add(keyStr);
            personDict[key] = student;
            stringDict[keyStr] = student;
        }
    }

    // вимірювання часу пошуку для 4 елементів
    public void MeasureSearchTime(int count)
    {
        // перший, центральний, останній, відсутній елемент
        int[] indices = new int[]
        {
            0,
            count / 2,
            count - 1,
            -1
        };

        string[] labels = new string[]
        {
            "First",
            "Middle",
            "Last",
            "Not in collection"
        };

        Stopwatch sw = new Stopwatch();

        for (int i = 0; i < indices.Length; i++)
        {
            Student searchTarget;
            Person personKey;
            string stringKey;

            if (indices[i] == -1)
            {
                // елемент, якого немає в колекції
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

            // List<Person> Contains
            sw.Restart();
            bool r1 = personList.Contains(personKey);
            sw.Stop();
            Console.WriteLine(
                $"List<Person>.Contains: {sw.ElapsedTicks} ticks, found={r1}"
            );

            // List<string> Contains
            sw.Restart();
            bool r2 = stringList.Contains(stringKey);
            sw.Stop();
            Console.WriteLine(
                $"List<string>.Contains: {sw.ElapsedTicks} ticks, found={r2}"
            );

            // Dictionary<Person, Student> ContainsKey
            sw.Restart();
            bool r3 = personDict.ContainsKey(personKey);
            sw.Stop();
            Console.WriteLine(
                $"Dictionary<Person,Student>.ContainsKey: {sw.ElapsedTicks} ticks, found={r3}"
            );

            // Dictionary<string, Student> ContainsKey
            sw.Restart();
            bool r4 = stringDict.ContainsKey(stringKey);
            sw.Stop();
            Console.WriteLine(
                $"Dictionary<string,Student>.ContainsKey: {sw.ElapsedTicks} ticks, found={r4}"
            );

            // Dictionary<Person, Student> ContainsValue
            sw.Restart();
            bool r5 = personDict.ContainsValue(searchTarget);
            sw.Stop();
            Console.WriteLine(
                $"Dictionary<Person,Student>.ContainsValue: {sw.ElapsedTicks} ticks, found={r5}"
            );

            Console.WriteLine();
        }
    }
}
