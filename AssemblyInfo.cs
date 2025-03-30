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