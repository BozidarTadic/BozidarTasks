using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Tasks.Common
{
    public class Response<T> : IResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
        public List<ErrorResult> ErrorMessage { get; set; }
    }
}
