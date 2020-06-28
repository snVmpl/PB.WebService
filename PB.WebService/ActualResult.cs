using System.Net;

namespace PB.WebService
{
    /// <summary>
    /// Content for requests
    /// </summary>
    public class ActualResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ActualResult(HttpStatusCode statusCode = HttpStatusCode.OK, string message = null, object data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
