// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Examples.AspNetCore.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    private readonly SharedObject sharedObject;

    public SampleHealthCheck(SharedObject sharedObject)
    {
        this.sharedObject = sharedObject;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (this.sharedObject.IsHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}
