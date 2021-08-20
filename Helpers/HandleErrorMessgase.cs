using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using University.Models.Responses;

namespace University.Helpers
{
    public class HandleErrorMessgase : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorInModelState = context.ModelState
                    .Where(a => a.Value.Errors.Count > 0)
                    .ToDictionary(k => k.Key,
                        k => k.Value.Errors.Select(a => a.ErrorMessage)).ToArray();
                var errorResponse = new List<ErrorModel>();
                foreach (var error in errorInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        errorResponse.Add(new ErrorModel()
                        {
                            FieldName = error.Key,
                            Message = subError
                        });
                    }
                }
                context.Result = new BadRequestObjectResult(new ErrorMessages()
                {
                    StatusCode = "000",
                    Message = "ModelState Invalid",
                    FieldError = errorResponse
                });
            }

            await next();
        }
    }
}
