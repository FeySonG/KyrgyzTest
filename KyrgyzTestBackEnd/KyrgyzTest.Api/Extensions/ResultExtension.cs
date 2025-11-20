using System.Runtime.InteropServices.JavaScript;
using KyrgyzTest.Application.Extensions.Result;
using Microsoft.AspNetCore.Mvc;

namespace SibersCRM.API.Extensions;


public static class ResultExtension
{
    public static IActionResult Match<TValue>(
        this Result<TValue> result,
        Func<TValue?, IActionResult> onSuccess,
        Func<Error, IActionResult> onFailure)
    {
        if (result.IsSuccess)
            return onSuccess(result.Value);

        return onFailure(result.Error!);
    }
}