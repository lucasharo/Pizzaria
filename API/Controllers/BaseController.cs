using System;
using System.Net;
using API.Models;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }
        
        [NonAction]
        private ObjectResult CreateErrorResponse<T>(string message)
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    MessageError = message
                });
        }

        [NonAction]
        private ObjectResult CreateSuccessResponse<T>(T data, string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    MessageError = message,
                    Result = data
                });
        }

        [NonAction]
        private ObjectResult CreateNotFoundResponse<T>(string message = "")
        {
            return Ok(
                new BaseResponse<T>
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    MessageError = message
                });
        }

        [NonAction]
        public ActionResult Execute<T>(Func<T> action)
        {
            try
            {
                return CreateSuccessResponse<T>(action());
            }
            catch (AppException ex)
            {
                return CreateNotFoundResponse<T>(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return CreateErrorResponse<T>(ex.Message);
            }            
        }
    }
}