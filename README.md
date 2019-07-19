# aop_logging

Simple aspect oriented logger


The logging is implemented in `LogginProxy` class. To complete the logging an implementation of the `ILogger` and `ILogEntryGenerator` have to be passed in next to the `T` what we want to decorate with the loggin.


Example Usage

```csharp
var c = LoggingProxy<ICalculator>.Create(new Calculator(), new ConsoleLogger(), new LogEntryGenerator());
c.Add(1, 2);
```

If we decorate `T` with the `LoggingProxy` the logging will be totally automatic.
