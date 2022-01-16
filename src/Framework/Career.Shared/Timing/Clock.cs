using System;

namespace Career.Shared.Timing;

public static class Clock
{
    public static IClockProvider Provider
    {
        get => _provider;
        set => _provider = value ?? throw new ArgumentNullException(nameof(value), "Can not set Clock.Provider to null!");
    }

    private static IClockProvider _provider;

    static Clock()
    {
        Provider = ClockProviders.Unspecified;
    }

    public static DateTime Now => Provider.Now;

    public static DateTimeKind Kind => Provider.Kind;

    public static bool SupportsMultipleTimezone => Provider.SupportsMultipleTimezone;

    public static DateTime Normalize(DateTime dateTime)
    {
        return Provider.Normalize(dateTime);
    }
}