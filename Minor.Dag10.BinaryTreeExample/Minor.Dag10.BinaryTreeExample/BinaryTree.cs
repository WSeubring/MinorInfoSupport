using System;

public abstract class BinaryTree
{
    public abstract int Count { get; }
    public abstract int Depth { get; }

    public static BinaryTree Empty { get { return new Empty(); } }

    public virtual BinaryTree Add(int newValue)
    {
        return new Branch(newValue);
    }
}