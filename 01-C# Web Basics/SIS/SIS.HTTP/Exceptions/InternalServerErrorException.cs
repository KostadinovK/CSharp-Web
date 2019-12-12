using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string DefaultMessage = "The Server has encountered an error.";

        public InternalServerErrorException() : base(DefaultMessage) { }

        public InternalServerErrorException(string message) : base(message) { }
    }
}
