using System;
using System.Collections.Generic;
using System.Text;

namespace OS.Core.Api.Middleware
{
    public static class Constants
    {
        public static class RequestCorrelation
        {
            public const string RequestHeaderName = "OpenSmog-CorrelationId";
            public const string LogPropertyName = "CorrelationId";
        }
    }
}
