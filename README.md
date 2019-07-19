# aop_logging

Simple aspect oriented logger


The logging is implemented in `LogginProxy` class. To complete the logging an implementation of the `ILogger` and `ILogEntryGenerator` have to be passed in next to the `T` what we want to decorate with the loggin.


Example Usage

```csharp
                                            // the Calculator has to implement an interface
var calc = LoggingProxy<ICalculator>.Create(new Calculator(),
                                            // your implementation of AopLogging.ILogger
                                            new ConsoleLogger(),
                                            // your or the built-in implemenation
                                            // of AopLogging.ILogEntryGenerator
                                            new LogEntryGenerator());

calc.Add(1, 2);
```

If we decorate `T` with the `LoggingProxy` the logging will be totally automatic.

Sample output of `ConsoleLogger`

```
07/19/2019 19:32:35|Information|Invoke|FullName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2
07/19/2019 19:32:35|Information|Leave|FullName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2, Return type: System.Int32, Return value: 3
```
