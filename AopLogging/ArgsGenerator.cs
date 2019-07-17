namespace AopLogging
{
    public class ArgsGenerator : IArgsGenerator
    {
        public string ToFlatString(object[] args)
        {
            return string.Join(Constants.ArrayDelimiter, args);
        }
    }
}