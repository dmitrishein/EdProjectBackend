using System;
using System.Collections.Generic;
using System.Net;

namespace EdProject.BLL
{
    [Serializable]
    public class CustomException : Exception
    {
        public List<string> Errors { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public CustomException(List<string> errors, HttpStatusCode statusCode) 
        {
            StatusCode = statusCode;
            Errors = new List<string>(errors);
        }
        public CustomException(string message,HttpStatusCode statusCode) 
        {
            Errors = new List<string>();
            Errors.Add(message);
            StatusCode = statusCode;
        }

    }
}
