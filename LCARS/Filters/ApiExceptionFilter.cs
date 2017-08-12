using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using LCARS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LCARS.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is AuthenticationException)
            {
                CreateResponse(context, HttpStatusCode.Unauthorized);
            }
            else if (context.Exception is ArgumentException)
            {
                CreateResponse(context, HttpStatusCode.BadRequest);
            }
            else if (context.Exception is HttpRequestException)
            {
                CreateResponse(context, HttpStatusCode.BadGateway);
            }
            else
            {
                CreateResponse(context, HttpStatusCode.InternalServerError);
            }

            base.OnException(context);
        }

        private static void CreateResponse(ExceptionContext context, HttpStatusCode httpStatusCode)
        {
            var response = new ApiError
            {
                Errors = new List<ApiErrorItem>()
            };

            response.Errors.Add(
                new ApiErrorItem
                {
                    Code = ((int)httpStatusCode).ToString(),
                    Status = httpStatusCode.ToString(),
                    Title = context.Exception.GetBaseException().Message,
                    Detail = context.Exception.GetBaseException().Message,
                    Source = context.Exception.GetBaseException().Source
                });

            context.HttpContext.Response.StatusCode = (int)httpStatusCode;
            context.Result = new JsonResult(response);
        }
    }
}