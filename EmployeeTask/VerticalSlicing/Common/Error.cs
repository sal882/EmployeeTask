﻿namespace EmployeeTask.VerticalSlicing.Common
{
    public record Error(string Description, int? StatusCode)
    {
        public static readonly Error None = new(string.Empty, null);
    }
}
