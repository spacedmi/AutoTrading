using System;

namespace AutoTrading.Common.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
        DateTimeOffset UtcNow { get; }
    }
}