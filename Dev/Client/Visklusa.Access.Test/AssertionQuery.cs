using NUnit.Framework.Constraints;

namespace Visklusa.Access.Test;

internal class AssertionQuery<T>
{
    public required T Subject { private get; init; }

    public AssertionQuery<T> AssertThat<TActual>(Func<T, TActual> selector, Constraint constraint)
    {
        Assert.That(selector(Subject), constraint);
        return this;
    }

    public AssertionQuery<T> OnSequence<TActual>(Func<T, IEnumerable<TActual>> selector,
        params Action<AssertionQuery<TActual>>[] childAssertions)
    {
        var array = selector(Subject).ToArray();
        Assert.That(array.Length, Is.EqualTo(childAssertions.Length));

        for (int i = 0; i < array.Length; i++)
        {
            childAssertions[i].Invoke(new AssertionQuery<TActual>()
            {
                Subject = array[i]
            });
        }

        return this;
    }
}

internal static class AssertionQuery
{
    public static AssertionQuery<T> OnObject<T>(T subject)
    {
        return new AssertionQuery<T>()
        {
            Subject = subject
        };
    }
}