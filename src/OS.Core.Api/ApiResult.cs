using System;

namespace OS.Core
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
