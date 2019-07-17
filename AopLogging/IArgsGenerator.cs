namespace AopLogging
{
    public interface IArgsGenerator
    {
        string ToFlatString(object[] args);
    }
}