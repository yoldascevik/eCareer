namespace Career.Extensions.System;

public static class TypeExtension
{
    public static T As<T>(this object instance) => (T)instance;
}