using System;
using SharedKernel;

namespace Infrastructure.Time;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow =>  DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
}
