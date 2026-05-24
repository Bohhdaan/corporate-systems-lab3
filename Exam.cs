using System;

public class Exam : IDateAndCopy
{
    public string Subject { get; set; }

    public int Grade { get; set; }

    public DateTime ExamDate { get; set; }

    public DateTime Date
    {
        get => ExamDate;
        init => ExamDate = value;
    }

    public Exam()
    {
        Subject = "Math";
        Grade = 100;
        ExamDate = DateTime.Now;
    }

    public Exam(
        string subject,
        int grade,
        DateTime examDate
    )
    {
        Subject = subject;
        Grade = grade;
        ExamDate = examDate;
    }

    public object DeepCopy()
    {
        return new Exam(
            Subject,
            Grade,
            ExamDate
        );
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Exam exam)
        {
            return false;
        }

        return Subject == exam.Subject &&
               Grade == exam.Grade &&
               ExamDate == exam.ExamDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            Subject,
            Grade,
            ExamDate
        );
    }

    public static bool operator ==(
        Exam? left,
        Exam? right
    )
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Equals(right);
    }

    public static bool operator !=(
        Exam? left,
        Exam? right
    )
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return $"Subject: {Subject}, " +
               $"Grade: {Grade}, " +
               $"Date: " +
               $"{ExamDate.ToShortDateString()}";
    }
}