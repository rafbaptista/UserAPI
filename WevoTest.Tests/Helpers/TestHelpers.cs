using System.IO;
using System.Reflection;

namespace WevoTest.Tests.Helpers
{
    public static class TestHelpers
    {
        public static string TestDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);        
    }
}
