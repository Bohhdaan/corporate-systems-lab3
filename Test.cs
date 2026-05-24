using System;

public class Test : IDateAndCopy
{
    public string Subject { get; set; }

    public bool IsPassed { get; set; }

    public DateTime TestDate { get; set; }

    public DateTime Date
    {
        get => TestDate;
        init => TestDate = value;
    }

    public Test()
    {
        Subject = "Programming";
        IsPassed = true;
        TestDate = DateTime.Now;
    }

    public Test(
        string subject,
        bool isPassed
    )
    {
        Subject = subject;
        IsPassed = isPassed;
        TestDate = DateTime.Now;
    }

    public object DeepCopy()
    {
        return new Test(
            Subject,
            IsPassed
        )
        {
            TestDate = TestDate
        };
    }

public override bool Equals(object? obj)
{
    if (obj is not Test test)
        return false;

    return Subject == test.Subject &&
       IsPassed == test.IsPassed;
}

public override int GetHashCode()
{
    return HashCode.Combine(
    Subject,
    IsPassed
);
}

public static bool operator ==(Test? left, Test? right)
{
    if (ReferenceEquals(left, right))
        return true;

    if (left is null || right is null)
        return false;

    return left.Equals(right);
}

public static bool operator !=(Test? left, Test? right)
{
    return !(left == right);
    
}
    public override string ToString()
    {
        return $"Subject: {Subject}, " +
               $"Passed: {IsPassed}";
    }
}