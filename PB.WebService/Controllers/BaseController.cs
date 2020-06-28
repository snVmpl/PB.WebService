using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace PB.WebService.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly ActualResult ActualResult;

        public BaseController()
        {
            ActualResult = new ActualResult();
        }

        protected IActionResult Result()
        {
            return Ok(ActualResult);
        }

        protected void SetExceptionResult(Exception ex)
        {
            ActualResult.StatusCode = HttpStatusCode.BadRequest;
            ActualResult.Message = ex.Message;
        }
    }
}