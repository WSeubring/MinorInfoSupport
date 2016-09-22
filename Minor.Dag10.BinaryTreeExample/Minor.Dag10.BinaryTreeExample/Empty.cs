using System;
using System.Collections.Generic;

internal class Empty : BinaryTree
{
    public override int Count
    {
        get
        {
            return 0;
        }
    }

    public override int Depth
    {
        get
        {
            return 0;
        }
    }

    public override bool Contains(int value)
    {
        return false;
    }

    public override IEnumerator<int> GetEnumerator()
    {
        yield return 0;
    }
}