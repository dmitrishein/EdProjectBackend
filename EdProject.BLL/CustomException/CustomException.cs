using System;
using System.Net;

namespace EdProject.BLL
{
    [Serializable]
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public CustomException() { }
        public CustomException(string message,HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

    }
}
