using Bogus;

namespace Company.Tests.Helpers;

internal static class FakerExtensions
{
    internal static string TaxNumber(this Bogus.DataSets.Company company, int length = 11)
    {
        return new Faker().Random.String2(length, "1234567890");
    }
}