using System.Collections.Generic;

// компаратор студентів по середньому балу
public class StudentAverageMarkComparer : IComparer<Student>
{
    public int Compare(Student? x, Student? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;
        return x.AverageMark.CompareTo(y.AverageMark);
    }
}
