// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Examples.AspNetCore.HealthChecks;

public class SampleHealthCheck : IHealthCheck
{
    private SharedObject _sharedObject;

    public SampleHealthCheck(SharedObject sharedObject)
    {
        this._sharedObject = sharedObject;
    }

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        if (this._sharedObject.IsHealthy)
        {
            return Task.FromResult(
                HealthCheckResult.Healthy("A healthy result."));
        }

        return Task.FromResult(
            new HealthCheckResult(
                context.Registration.FailureStatus, "An unhealthy result."));
    }
}
