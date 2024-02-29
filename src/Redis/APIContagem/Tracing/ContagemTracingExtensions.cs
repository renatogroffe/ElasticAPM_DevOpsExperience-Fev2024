using System.Runtime.InteropServices;

namespace APIContagem.Tracing;

public static class ContagemTracingExtensions
{
    public static string Local { get; }
    public static string Kernel { get; }
    public static string Framework { get; }

    static ContagemTracingExtensions()
    {
        Local = "APIContagemRedis";
        Kernel = Environment.OSVersion.VersionString;
        Framework = RuntimeInformation.FrameworkDescription;
    }
}