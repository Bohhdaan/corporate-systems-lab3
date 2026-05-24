using System;

public interface IDateAndCopy
{
    DateTime Date { get; init; }

    object DeepCopy();
}