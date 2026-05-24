using System;
using System.Collections;
using System.Collections.Generic;

public class StudentEnumerator : IEnumerator
{
    private List<string> subjects;
    private int position = -1;

    public StudentEnumerator(
        List<Test> tests,
        List<Exam> exams
    )
    {
        subjects = new List<string>();

        foreach (Test test in tests)
        {
            foreach (Exam exam in exams)
            {
                if (test.Subject == exam.Subject &&
                    !subjects.Contains(test.Subject))
                {
                    subjects.Add(test.Subject);
                }
            }
        }
    }

    public object Current => subjects[position]!;

    public bool MoveNext()
    {
        position++;
        return position < subjects.Count;
    }

    public void Reset()
    {
        position = -1;
    }
}
