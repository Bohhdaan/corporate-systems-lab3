using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Student :
    Person,
    IDateAndCopy,
    IEnumerable,
    IComparable<Student>
{
    private Education education;
    private int groupNumber;
    private List<Test> tests;
    private List<Exam> exams;

    public Student()
        : base()
    {
        education = Education.Bachelor;
        groupNumber = 101;
        tests = new List<Test>();
        exams = new List<Exam>();
    }

    public Student(
        Person person,
        Education education,
        int groupNumber
    )
        : base(person.Name, person.Surname, person.BirthDate)
    {
        this.education = education;
        GroupNumber = groupNumber;
        tests = new List<Test>();
        exams = new List<Exam>();
    }

    public Person Person
    {
        get => new Person(name, surname, birthDate);
        init
        {
            name = value.Name;
            surname = value.Surname;
            birthDate = value.BirthDate;
        }
    }

    public Education Education
    {
        get => education;
        init => education = value;
    }

    public int GroupNumber
    {
        get => groupNumber;

        init
        {
            if (value < 101 || value > 699)
            {
                throw new ArgumentException(
                    "Group number must be in range 101-699"
                );
            }
            groupNumber = value;
        }
    }

    public List<Test> Tests
    {
        get => tests;
        init => tests = value;
    }

    public List<Exam> Exams
    {
        get => exams;
        init => exams = value;
    }

    public double AverageMark
    {
        get
        {
            if (exams.Count == 0) return 0;
            double sum = 0;
            foreach (Exam exam in exams)
                sum += exam.Grade;
            return sum / exams.Count;
        }
    }

    public bool this[Education edu]
    {
        get => education == edu;
    }

    public void AddExams(params Exam[] newExams)
    {
        foreach (Exam exam in newExams)
            exams.Add(exam);
    }

    public void AddTests(params Test[] newTests)
    {
        foreach (Test test in newTests)
            tests.Add(test);
    }

    // порівняння по прізвищу — використовує IComparable з Person
    public int CompareTo(Student? other)
    {
        if (other == null) return 1;
        return base.CompareTo(other);
    }

    public override object DeepCopy()
    {
        Student copy = new Student(
            new Person(name, surname, birthDate),
            education,
            groupNumber
        );

        foreach (Test test in tests)
            copy.tests.Add((Test)test.DeepCopy());

        foreach (Exam exam in exams)
            copy.exams.Add((Exam)exam.DeepCopy());

        return copy;
    }

    public override bool Equals(object? obj)
    {
    if (obj is not Student student)
        return false;

    if (!base.Equals(student))
        return false;

    return education == student.education &&
           groupNumber == student.groupNumber &&
           tests.SequenceEqual(student.tests) &&
           exams.SequenceEqual(student.exams);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), education, groupNumber);
    }

    public static bool operator ==(Student? left, Student? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Student? left, Student? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        string examsInfo = "";
        string testsInfo = "";

        foreach (Exam exam in exams)
            examsInfo += exam + "\n";

        foreach (Test test in tests)
            testsInfo += test + "\n";

        return $"Student information:\n" +
               $"{base.ToString()}\n" +
               $"Education: {education}\n" +
               $"Group number: {groupNumber}\n" +
               $"Average mark: {AverageMark}\n\n" +
               $"Tests:\n{testsInfo}\n" +
               $"Exams:\n{examsInfo}";
    }

    public override string ToShortString()
    {
        return $"Student: {base.ToShortString()}\n" +
               $"Education: {education}\n" +
               $"Group: {groupNumber}\n" +
               $"Average mark: {AverageMark}";
    }

    public IEnumerable GetAllExamsAndTests()
    {
        foreach (object test in tests)
            yield return test;
        foreach (object exam in exams)
            yield return exam;
    }

    public IEnumerable GetExamsWithGradeHigher(int value)
    {
        foreach (Exam exam in exams)
        {
            if (exam.Grade > value)
                yield return exam;
        }
    }

    public IEnumerable GetPassedExamsAndTests()
    {
        foreach (Test test in tests)
        {
            if (test.IsPassed)
                yield return test;
        }
        foreach (Exam exam in exams)
        {
            if (exam.Grade > 2)
                yield return exam;
        }
    }

    public IEnumerable GetPassedTestsWithExams()
    {
        foreach (Test test in tests)
        {
            if (!test.IsPassed) continue;
            foreach (Exam exam in exams)
            {
                if (test.Subject == exam.Subject && exam.Grade > 2)
                    yield return test;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new StudentEnumerator(tests, exams);
    }
}
