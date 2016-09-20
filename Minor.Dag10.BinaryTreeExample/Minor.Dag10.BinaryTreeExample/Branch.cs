using System;

internal class Branch : BinaryTree
{
    public int Value { get; set; }
    public BinaryTree Right { get; set; }
    public BinaryTree Left { get; set; }

    public override int Count
    {
        get
        {
            return 1 + Left.Count + Right.Count;
        }
    }

    public override int Depth
    {
        get
        {
            return 1 + Math.Max(Left.Depth, Right.Depth);
        }
    }

    public Branch(int value)
    {
        Value = value;
        Left = new Empty();
        Right = new Empty();
    }

    public override BinaryTree Add(int newValue)
    {
        if( Value == newValue)
        {
            return this;
        }
        else if(newValue < Value)
        {
            Left = new Branch(newValue);
        }
        else if(newValue > Value)
        {
            Right = new Branch(newValue);
        }
        return this;
    }
}