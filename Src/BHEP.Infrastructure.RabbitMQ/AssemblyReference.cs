using System.Reflection;

namespace BHEP.Infrastructure.RabbitMQ;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

