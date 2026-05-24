using System;
using System.Collections.Generic;
using System.Linq;

public class StudentCollection
{
    private List<Student> students;

    public StudentCollection()
    {
        students = new List<Student>();
    }

    public void AddDefaults()
    {
        students.Add(new Student(
            new Person("Olena", "Kovalenko", new DateTime(2002, 3, 15)),
            Education.Bachelor, 201
        ));
        students.Add(new Student(
            new Person("Ivan", "Melnyk", new DateTime(2001, 7, 22)),
            Education.Master, 301
        ));
        students.Add(new Student(
            new Person("Maria", "Shevchenko", new DateTime(2003, 1, 5)),
            Education.Bachelor, 202
        ));
    }

    public void AddStudents(params Student[] newStudents)
    {
        foreach (Student s in newStudents)
            students.Add(s);
    }

    // максимальний середній бал (тільки читання)
    public double MaxAverageMark
    {
        get
        {
            if (students.Count == 0) return 0;
            return students.Max(s => s.AverageMark);
        }
    }

    // студенти з формою навчання Master
    public IEnumerable<Student> MasterStudents
    {
        get => students.Where(s => s.Education == Education.Master);
    }

    // список студентів із заданим середнім балом
    public List<Student> AverageMarkGroup(double value)
    {
        return students
            .GroupBy(s => s.AverageMark)
            .Where(g => g.Key == value)
            .SelectMany(g => g)
            .ToList();
    }

    // всі групи по середньому балу
    public IEnumerable<IGrouping<double, Student>> GroupedByAverageMark()
    {
        return students.GroupBy(s => s.AverageMark);
    }

    // сортування по прізвищу - IComparable<Student> -> Person.CompareTo (по прізвищу)
    public void SortBySurname()
    {
        students.Sort();
    }

    // сортування по даті народження - IComparer<Person> реалізований в Person.BirthDateComparer
    public void SortByBirthDate()
    {
        Person.BirthDateComparer comparer = new Person.BirthDateComparer();
        students.Sort((x, y) => comparer.Compare(x.Person, y.Person));
    }

    // сортування по середньому балу — IComparer<Student>
    public void SortByAverageMark()
    {
        students.Sort(new StudentAverageMarkComparer());
    }

    public override string ToString()
    {
        string result = "";
        foreach (Student s in students)
            result += s.ToString() + "\n\n";
        return result;
    }

    public string ToShortString()
    {
        string result = "";
        foreach (Student s in students)
        {
            result += s.ToShortString() +
                      $"\nTests: {s.Tests.Count}, " +
                      $"Exams: {s.Exams.Count}\n\n";
        }
        return result;
    }
}
