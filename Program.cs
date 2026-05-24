using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        
        // StudentCollection
        
        Console.WriteLine("=== StudentCollection ===\n");

        StudentCollection collection = new StudentCollection();
        collection.AddDefaults();

        Student s1 = new Student(
            new Person("Bohdan", "Iliuk", new DateTime(2006, 3, 25)),
            Education.Master, 311
        );
        s1.AddExams(
            new Exam("Math", 5, new DateTime(2025, 5, 10)),
            new Exam("Databases", 4, new DateTime(2025, 5, 12))
        );
        s1.AddTests(new Test("Algorithms", true));

        Student s2 = new Student(
            new Person("Anna", "Petrenko", new DateTime(2005, 5, 5)),
            Education.Master, 312
        );
        s2.AddExams(
            new Exam("Math", 3, new DateTime(2025, 5, 10)),
            new Exam("Physics", 5, new DateTime(2025, 5, 14))
        );
        s2.AddTests(new Test("Programming", true));

        Student s3 = new Student(
            new Person("Dmytro", "Zinchenko", new DateTime(2005, 11, 25)),
            Education.Bachelor, 215
        );
        s3.AddExams(
            new Exam("Math", 4, new DateTime(2025, 5, 10))
        );
        s3.AddTests(new Test("Algorithms", false));

        collection.AddStudents(s1, s2, s3);

        Console.WriteLine("Initial collection (short):");
        Console.WriteLine(collection.ToShortString());

        // сортування по прізвищу
        collection.SortBySurname();
        Console.WriteLine("Sorted by surname:");
        Console.WriteLine(collection.ToShortString());

        // сортування по даті народження
        collection.SortByBirthDate();
        Console.WriteLine("Sorted by birth date:");
        Console.WriteLine(collection.ToShortString());

        // сортування по середньому балу
        collection.SortByAverageMark();
        Console.WriteLine("Sorted by average mark:");
        Console.WriteLine(collection.ToShortString());

        // максимальний середній бал
        Console.WriteLine($"Max average mark: {collection.MaxAverageMark}\n");

        // фільтрація — тільки Master
        Console.WriteLine("Master students:");
        foreach (Student s in collection.MasterStudents)
            Console.WriteLine(s.ToShortString() + "\n");

        // групування по середньому балу
        Console.WriteLine("Students grouped by average mark:");
        foreach (var group in collection.GroupedByAverageMark())
        {
            Console.WriteLine($"  Average mark = {group.Key:F2}:");
            foreach (Student s in group)
                Console.WriteLine($"    {s.ToShortString()}");
            Console.WriteLine();
        }

        
        // TestCollections
        
        Console.WriteLine("=== TestCollections ===\n");

        int n = 0;
        while (true)
        {
            Console.Write("Enter number of elements in collections: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out n) && n > 0)
                break;
            Console.WriteLine("Invalid input, please enter a positive integer.");
        }

        TestCollections tc = new TestCollections(n);
        tc.MeasureSearchTime(n);
    }
}
