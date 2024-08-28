using System.Reflection;

namespace BHEP.Infrastructure.BlobStorage;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
