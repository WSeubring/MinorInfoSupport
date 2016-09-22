using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BinaryTree : IEnumerable<int>
{
    public abstract int Count { get; }
    public abstract int Depth { get; }
    public abstract bool Contains(int value);
    public static BinaryTree Empty { get { return new Empty(); } }

    public virtual BinaryTree Add(int newValue)
    {
        return new Branch(newValue);
    }

    public abstract IEnumerator<int> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}