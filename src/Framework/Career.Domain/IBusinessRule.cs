namespace Career.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();
        string Message { get; }
    }
}