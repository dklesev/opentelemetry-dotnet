// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

namespace Examples.AspNetCore.HealthChecks;

public class SharedObject
{
    public bool IsHealthy
    {
        get;
        set;
    } = true;
}
