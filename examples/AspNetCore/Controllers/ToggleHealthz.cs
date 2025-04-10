// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using Examples.AspNetCore;
using Examples.AspNetCore.HealthChecks;
using Microsoft.AspNetCore.Mvc;

namespace Examples.AspNetCore.Controllers;

[ApiController]
[Route("[controller]")]
public class ToggleHealthz : ControllerBase
{
    private SharedObject _healthCheck;

    public ToggleHealthz(SharedObject check)
    {
        this._healthCheck = check;
    }

    [HttpGet]
    public bool ToggleHealthzState()
    {
        this._healthCheck.IsHealthy = !this._healthCheck.IsHealthy;
        return true;
    }
}
