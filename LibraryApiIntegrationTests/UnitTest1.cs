using System;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int a = 10, b = 20;
            int answer = a + b;
            Assert.Equal(30, answer);
        }

        [Fact]
        public void AnotherTest()
        {
            int a = 10, b = 20;
            int answer = a + b;
            Assert.Equal(30, answer);
        }
    }
}
