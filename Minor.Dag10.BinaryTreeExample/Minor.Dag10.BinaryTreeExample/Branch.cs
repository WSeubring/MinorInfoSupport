using System;
using System.Collections;
using System.Collections.Generic;

internal class Branch : BinaryTree
{
    private int _value;
    private BinaryTree _right;
    private BinaryTree _left;

    public override int Count
    {
        get
        {
            return 1 + _left.Count + _right.Count;
        }
    }

    public override int Depth
    {
        get
        {
            return 1 + Math.Max(_left.Depth, _right.Depth);
        }
    }

    public Branch(int value)
    {
        _value = value;
        _left = new Empty();
        _right = new Empty();
    }

    public override BinaryTree Add(int newValue)
    {
        if( _value == newValue)
        {
            return this;
        }
        else if(newValue < _value)
        {
            _left = new Branch(newValue);
        }
        else if(newValue > _value)
        {
            _right = new Branch(newValue);
        }
        return this;
    }

    public override bool Contains(int value)
    {
        if(_value == value)
        {
            return true;
        }
        else if (value < _value)
        {
            return _left.Contains(value);
        } 
        else
        {
            return _right.Contains(value);
        }
    }

    public override IEnumerator<int> GetEnumerator()
    {
        if (!(_left is Empty))
        {
            foreach (var value in _left)
            {
                yield return value;
            }
        }

        yield return _value;

        if (!(_right is Empty))
        {
            foreach (var value in _right)
            {
                yield return value;
            }
        }

    }
}