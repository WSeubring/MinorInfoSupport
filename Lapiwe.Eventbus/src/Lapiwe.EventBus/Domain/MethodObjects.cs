using System;
using System.Reflection;

namespace Lapiwe.EventBus.Domain
{
    public class MethodObject
    {
        public Type type { get; set; }
        public MethodInfo methodInfo { get; set; }
    }
}