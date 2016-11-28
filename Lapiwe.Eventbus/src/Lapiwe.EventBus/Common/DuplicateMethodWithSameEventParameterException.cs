using System;
using System.Reflection;

namespace Lapiwe.EventBus.Common
{
    public class DuplicateMethodWithSameEventParameterException : Exception
    {

        public DuplicateMethodWithSameEventParameterException()
        {
        }

        public DuplicateMethodWithSameEventParameterException(string message) : base(message)
        {
        }

        public DuplicateMethodWithSameEventParameterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}