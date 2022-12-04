using NUnit.Framework.Constraints;

namespace Visklusa.Access.Test;

internal class AssertionContext<T>
{
    public required T Subject { private get; init; }

    public AssertionContext<T> AssertThat<TActual>(Func<T, TActual> selector, Constraint constraint)
    {
        Assert.That(selector(Subject), constraint);
        return this;
    }

    public AssertionContext<T> OnSequence<TActual>(Func<T, IEnumerable<TActual>> selector,
        params Action<AssertionContext<TActual>>[] childAssertions)
    {
        var array = selector(Subject).ToArray();
        Assert.That(array.Length, Is.EqualTo(childAssertions.Length));

        for (int i = 0; i < array.Length; i++)
        {
            childAssertions[i].Invoke(new AssertionContext<TActual>()
            {
                Subject = array[i]
            });
        }

        return this;
    }
}

internal static class AssertionContext
{
    public static AssertionContext<T> OnObject<T>(T subject)
    {
        return new AssertionContext<T>()
        {
            Subject = subject
        };
    }
}