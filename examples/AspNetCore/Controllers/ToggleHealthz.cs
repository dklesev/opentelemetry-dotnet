// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using Examples.AspNetCore.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace Examples.AspNetCore.Controllers;

[ApiController]
[Route("[controller]")]
public class ToggleHealthz : ControllerBase
{
    private readonly SharedObject healthCheck;

    public ToggleHealthz(SharedObject check)
    {
        this.healthCheck = check;
    }

    [HttpGet]
    public bool ToggleHealthzState()
    {
        this.healthCheck.IsHealthy = !this.healthCheck.IsHealthy;
        return true;
    }
}
