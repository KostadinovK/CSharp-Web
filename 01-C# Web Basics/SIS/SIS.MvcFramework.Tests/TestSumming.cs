using System;
using Xunit;

namespace SIS.MvcFramework.Tests
{
    public class TestSumming
    {
       [Fact]
       public void Sum2Plus2()
       {
           Assert.Equal(4, 2 + 2);
       }
    }
}
