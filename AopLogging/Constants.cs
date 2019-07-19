namespace AopLogging
{
    public static class Constants
    {
        public const string ArrayDelimiter = ", ";
        public const string KeyValueDelimiter = ": ";

        public static class PayloadKeys
        {
            public const string ClassNameKey = "ClassName";
            public const string MethodKey = "Method";
            public const string ArgsKey = "Args";
            public const string ReturnTypeKey = "Return type";
            public const string ReturnValueKey = "Return value";
            public const string ExceptionKey = "Exception";
        }
    }
}