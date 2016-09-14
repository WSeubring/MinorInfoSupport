using System;

public class MuntsoortNietOndersteundException : Exception
{
    public MuntsoortNietOndersteundException()
    {
    }

    public MuntsoortNietOndersteundException(string message) : base(message)
    {
    }

    public MuntsoortNietOndersteundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}