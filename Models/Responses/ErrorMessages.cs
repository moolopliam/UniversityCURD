using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Models.Responses
{
    public class ErrorMessages
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public List<ErrorModel> FieldError { get; set; } = new();
    }
}
