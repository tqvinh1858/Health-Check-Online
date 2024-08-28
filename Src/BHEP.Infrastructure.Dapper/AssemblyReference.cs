using System.Reflection;

namespace BHEP.Infrastructure.Dapper;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
