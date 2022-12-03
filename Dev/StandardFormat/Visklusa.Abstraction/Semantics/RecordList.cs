using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Visklusa.Abstraction.Semantics;

public record RecordList<TRecord>(TRecord[] Array) : IEnumerable<TRecord>
    where TRecord : notnull
{
    private List<TRecord> Items { get; } = Array.ToList();

    public RecordList() : this(System.Array.Empty<TRecord>())
    {
    }

    public void Add(TRecord item)
    {
        Items.Add(item);
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.AppendJoin(", ", Items);
        return true;
    }

    public virtual bool Equals(RecordList<TRecord>? other)
    {
        if (other is null) return false;

        if (Items.Count != other.Items.Count)
        {
            return false;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (!Items[i].Equals(other.Items[i]))
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return Items.GetHashCode() * nameof(RecordList<TRecord>).GetHashCode();
        }
    }

    public IEnumerator<TRecord> GetEnumerator() => ((IEnumerable<TRecord>)Items).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}