using System;
using System.Reflection;
using Career.Exceptions;
using Xunit;

namespace Job.Test.Helpers
{
    public static class CustomAsserts
    {
        public static void DeepEqual(object expected, object actual)
        {
            Check.NotNull(expected, nameof(expected));
            Check.NotNull(actual, nameof(actual));

            PropertyInfo[] inputDtoProperties = expected.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var inputDtoProperty in inputDtoProperties)
            {
                var responseProperty = actual.GetType().GetProperty(inputDtoProperty.Name);
                if (responseProperty != null)
                {
                    object expectedValue = inputDtoProperty.GetValue(expected);
                    object actualValue = responseProperty.GetValue(actual);

                    if (expectedValue != null && actualValue != null)
                    {
                        Type expectedType = expectedValue.GetType();
                        Type actualType = actualValue?.GetType();

                        if ((expectedType.IsValueType || expectedType == typeof(string)) && (actualType.IsValueType || actualType == typeof(string)))
                        {
                            Assert.Equal(expectedValue, actualValue);
                        }
                        else
                        {
                            DeepEqual(expectedValue, actualValue);
                        }
                    }
                    else
                    {
                        Assert.Equal(expectedValue, actualValue);
                    }
                }
            }
        }
    }
}