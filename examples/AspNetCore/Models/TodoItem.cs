// Copyright The OpenTelemetry Authors
// SPDX-License-Identifier: Apache-2.0

namespace Examples.AspNetCore.Models;

public class TodoItem
{
    public int? Id { get; set; }

    public required string Title { get; set; }

    public DateTime? DueDate { get; set; }

    public bool IsCompleted { get; set; }
}
