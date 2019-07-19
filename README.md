# aop_logging

Simple aspect oriented logger


The logging is implemented in `AopLogging.LogginProxy` class. To complete the logging, an implementation of the `AopLogging.ILogger` and `AopLogging.ILogEntryGenerator` have to be passed in next to the `T` what we want to decorate with the logging. The `T` class has to implement an interface, cannot be sealed, and cannot be abstract as these are the precondition of the `System.Reflection.DispatchProxyGenerator.GenerateProxyType(Type baseType, Type interfaceType)` method.

### Main Interfaces:

#### ILogger

The `ILogger` interface has just a signle function: `void Log(LogEntry logEntry)`. How the logging exactly happens is up to the user.

#### ILogEntryGenerator

Right now the code logs 3 type of events: Invoke, Leave, and Exception. Every event generates a `LogEntry` which will be logged by the `ILogger`. There is a default implementation of the `ILogEntryGenerator` which can be easily replaced by a custom implementation of the `ILogEntryGenerator` interface.

Example Usage

```csharp
var calc = LoggingProxy<ICalculator>.Create(new Calculator(),
                                            new ConsoleLogger(),
                                            new LogEntryGenerator());

calc.Add(1, 2);
```

If we decorate `T` with the `LoggingProxy` the logging will be totally automatic.

Sample output of `ConsoleLogger`

```
07/19/2019 19:32:35|Information|Invoke|FullName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2
07/19/2019 19:32:35|Information|Leave|FullName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2, Return type: System.Int32, Return value: 3
```
