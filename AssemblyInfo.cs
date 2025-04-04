using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
[assembly: Parallelize(Workers = 6, Scope = ExecutionScope.MethodLevel)]

namespace saucedemo
{
    [TestClass]
    public static class AssemblyHooks
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            var currentPath = Environment.GetEnvironmentVariable("PATH");
            string driverDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Utilities");
            Environment.SetEnvironmentVariable("PATH", $"{driverDirectory};{currentPath}");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Log.CloseAndFlush();
        }
    }
}