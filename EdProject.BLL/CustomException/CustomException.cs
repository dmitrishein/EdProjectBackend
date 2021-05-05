using System;

namespace EdProject.BLL
{
    [Serializable]
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }
        public CustomException() { }
        public CustomException(string message) : base(message) { }
        public CustomException(string message,Exception inner): base(message,inner) { }
        public CustomException(string message,int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
