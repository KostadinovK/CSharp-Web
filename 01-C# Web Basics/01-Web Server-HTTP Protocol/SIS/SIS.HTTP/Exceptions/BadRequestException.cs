using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string BadRequestDefaultMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException() : this(BadRequestDefaultMessage) { }

        public BadRequestException(string message) : base(message) { }
    }
}
