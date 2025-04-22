// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

namespace Examples.AspNetCore.Persistence.Extensions; // Corrected namespace

public class PostgresConfiguration
{
    public const string Section = "Postgres";

    public required string Host { get; set; }

    public required string Port { get; set; }

    public required string Database { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }
}
