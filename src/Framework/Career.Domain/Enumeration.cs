using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Career.Domain;

public abstract class Enumeration: Enumeration<int>
{
    protected Enumeration(int id, string name) : base(id, name)
    {
    }
}

public abstract class Enumeration<TId>
{
    public string Name { get; private set; }

    public TId Id { get; }

    protected Enumeration(TId id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration<TId>
    {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }
        
    public static bool operator ==(Enumeration<TId> obj1, Enumeration<TId> obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
            {
                return true;
            }
            return false;
        }
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Enumeration<TId> obj1, Enumeration<TId> obj2)
    {
        return !(obj1 == obj2);
    }

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration<TId>;

        if (otherValue == null)
            return false;

        bool typeMatches = GetType() == obj.GetType();
        bool valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static T FromValue<T>(TId value) where T : Enumeration<TId>
    {
        return Parse<T, TId>(value, "value", item => item.Id.Equals(value));
    }

    public static T FromName<T>(string displayName) where T : Enumeration<TId>
    {
        return Parse<T, string>(displayName, "display name", item => item.Name == displayName);
    }

    private static T Parse<T, TKey>(TKey value, string description, Func<T, bool> predicate) where T : Enumeration<TId>
    {
        T matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }
}