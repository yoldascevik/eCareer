namespace Career.Cache
{
    public struct TTLMultiplier
    {
        public const int Second = 1;
        public const int Minute = 60;
        public const int Hour = 60 * Minute;
        public const int Day = 24 * Hour;
        public const int Week = 7 * Day;
        public const int Year = 365 * Day;
    }
}