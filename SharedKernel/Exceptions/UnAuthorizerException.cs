using System;
using System.Globalization;

namespace SharedKernel.Exceptions;

public class UnAuthorizerException: Exception
    {
        public UnAuthorizerException() : base() { }

        public UnAuthorizerException(string message) : base(message) { }

        public UnAuthorizerException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }