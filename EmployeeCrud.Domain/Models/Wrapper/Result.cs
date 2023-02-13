using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeCrud.Domain.Models.Wrapper
{
    public class Result<T>
    {
        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public T Data { get; set; }
         

        public static Result<T> Fail()
        {
            return new Result<T> { Succeeded = false };
        }

        public static Result<T> Fail(HttpStatusCode statusCode, string message)
        {
            return new Result<T> { StatusCode = statusCode, Succeeded = false, Messages = new List<string> { message } };
        }

        public static Result<T> Success(T data)
        {
            return new Result<T> { Succeeded = true, Data = data };
        }
        public static Result<T> Success(string message)
        {
            return new Result<T> { Succeeded = true, Messages = new List<string> { message } };
        }
        public static Result<T> Success(T data, List<string> messages)
        {
            return new Result<T> { Succeeded = true, Data = data, Messages = messages };
        }


    }
}
