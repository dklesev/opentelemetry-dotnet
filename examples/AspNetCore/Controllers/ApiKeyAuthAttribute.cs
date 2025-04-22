// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Examples.AspNetCore.Controllers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKeyHeaderName = "X-API-KEY";

    public async Task OnActionExecutionAsync(ActionExecutingContext? context, ActionExecutionDelegate? next)
    {
        if (context != null)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var configuredApiKey = configuration.GetValue<string>("ApiKey");

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) ||
                string.IsNullOrEmpty(extractedApiKey) ||
                extractedApiKey != configuredApiKey)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        if (next != null)
        {
            await next().ConfigureAwait(false);
        }
    }
}
