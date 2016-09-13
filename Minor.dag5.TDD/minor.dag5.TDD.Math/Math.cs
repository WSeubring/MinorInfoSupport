using System;

public class Math
{
    public Math()
    {
    }

    public int Fact(int n)
    {   
        if (n <= 0)
        {
            throw new InvalidOperationException();
        }

        if (n == 1)
        {
            return 1;
        }
        return n * Fact(n - 1);
    }

    public double FindStrangeDouble()
    {
        return double.MaxValue;
    }
}