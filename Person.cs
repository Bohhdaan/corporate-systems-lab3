using System;
using System.Collections.Generic;

public class Person : IDateAndCopy, IComparable<Person>
{
    protected string name;
    protected string surname;
    protected DateTime birthDate;

    public Person()
    {
        name = "NoName";
        surname = "NoSurname";
        birthDate = new DateTime(2000, 1, 1);
    }

    public Person(
        string name,
        string surname,
        DateTime birthDate
    )
    {
        this.name = name;
        this.surname = surname;
        this.birthDate = birthDate;
    }

    public string Name
    {
        get => name;
        init => name = value;
    }

    public string Surname
    {
        get => surname;
        init => surname = value;
    }

    public DateTime BirthDate
    {
        get => birthDate;
        init => birthDate = value;
    }

    public int BirthYear
    {
        get => birthDate.Year;

        set
        {
            birthDate = new DateTime(
                value,
                birthDate.Month,
                birthDate.Day
            );
        }
    }

    public DateTime Date
    {
        get => birthDate;
        init => birthDate = value;
    }

    // порівняння по прізвищу
    public int CompareTo(Person? other)
    {
        if (other == null) return 1;
        return string.Compare(surname, other.surname, StringComparison.Ordinal);
    }

    public virtual object DeepCopy()
    {
        return new Person(name, surname, birthDate);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Person person) return false;
        return name == person.name &&
               surname == person.surname &&
               birthDate == person.birthDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(name, surname, birthDate);
    }

    public static bool operator ==(Person? left, Person? right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(Person? left, Person? right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"Name: {name}\n" +
               $"Surname: {surname}\n" +
               $"Birth date: {birthDate.ToShortDateString()}";
    }

    public virtual string ToShortString()
    {
        return $"{surname} {name}";
    }

    // компаратор по даті народження
    public class BirthDateComparer : IComparer<Person>
    {
        public int Compare(Person? x, Person? y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            return DateTime.Compare(x.birthDate, y.birthDate);
        }
    }
}
