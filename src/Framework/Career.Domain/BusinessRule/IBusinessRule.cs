namespace Career.Domain.BusinessRule;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}