﻿using LibraryAPI.Common.Enums;
using LibraryAPI.Common;
using Microsoft.AspNetCore.Mvc;
using LibraryAPI.Common.Response;

public static class OperationResultExtensions
{
    public static IActionResult ToActionResult<T>(this OperationResult<T> operationResult)
    {
        // SUCCESS
        if (operationResult.IsSuccessful)
        {
            return new OkObjectResult(new ResponseObject<T>
            {
                Data = operationResult.Data,
                Message = operationResult.Message,
                TotalCount = operationResult.TotalCount
            });
        }

        //ERROR
        var resObj = new ResponseObject<T>
        {
            Data = operationResult.Data,
            Message = operationResult.Message,
            TotalCount = operationResult.TotalCount
        };

        ObjectResult result;
        switch (operationResult.ErrorType)
        {
            case OperationErrorType.NotFound:
                result = new NotFoundObjectResult(resObj);
                break;

            case OperationErrorType.Conflict:
                result = new ConflictObjectResult(resObj);
                break;

            case OperationErrorType.BadRequest:
                result = new BadRequestObjectResult(resObj);
                break;

            case OperationErrorType.UnAuthorized:
                result = new UnauthorizedObjectResult(resObj);
                break;

            case OperationErrorType.InternalServerError:
                result = new ObjectResult(resObj);
                result.StatusCode = StatusCodes.Status500InternalServerError;
                break;

            case OperationErrorType.BadGateway:
                result = new ObjectResult(resObj);
                result.StatusCode = StatusCodes.Status502BadGateway;
                break;

            default:
                result = new ObjectResult(resObj);
                result.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        return result;
    }
}