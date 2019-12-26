using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework.Tests
{
    public class TestViewModel
    {
        public string StringValue { get; set; }

        public IEnumerable<string> ListValue { get; set; }
    }
}
