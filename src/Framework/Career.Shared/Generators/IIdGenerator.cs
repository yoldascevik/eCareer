namespace Career.Shared.Generators;

public interface IIdGenerator<TId>
{
    TId Generate();
}