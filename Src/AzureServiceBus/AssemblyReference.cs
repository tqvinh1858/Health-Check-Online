using System.Reflection;

namespace BHEP.Infrastructure.ServiceBus;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

