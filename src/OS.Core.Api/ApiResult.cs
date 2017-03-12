using System;

namespace OS.Core.Api
{
    public class ApiResult
    {
        public Guid CorrelationId { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {        
        public T Value { get; set; }
    }
        
}
