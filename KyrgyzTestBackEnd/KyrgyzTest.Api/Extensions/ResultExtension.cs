using KyrgyzTest.Application.Extensions.Result;
using Microsoft.AspNetCore.Mvc;

namespace KyrgyzTest.Api.Extensions;


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

    public static IActionResult Match(this Result<object> result)
    {
        throw new NotImplementedException();
    }
}