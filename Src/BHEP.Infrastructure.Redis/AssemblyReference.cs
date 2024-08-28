using System.Reflection;

namespace BHEP.Infrastructure.Redis;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

