# aop_logging

A Simple [aspect oriented](https://en.wikipedia.org/wiki/Aspect-oriented_programming "Aspect oriented programming") logger


The logging is implemented in `AopLogging.LogginProxy` class. To complete the logging, an implementation of the `AopLogging.ILogger` and `AopLogging.ILogEntryBuilder` have to be passed in next to the `T` what we want to decorate with the logging. The `T` class has to implement an interface, cannot be sealed, and cannot be abstract as these are the precondition of the `System.Reflection.DispatchProxyGenerator.GenerateProxyType(Type baseType, Type interfaceType)` method.

### Main Interfaces:

#### ILogger

The `ILogger` interface has just a signle function: `void Log(LogEntry logEntry)`. How the logging exactly happens is up to the user.

#### ILogEntryGenerator

Right now the code logs 3 type of events: Invoke, Leave, and Exception. Every event generates a `LogEntry` which will be logged by the `ILogger`. There is a default implementation of the `ILogEntryBuilder` which can be easily replaced by a custom implementation of the `ILogEntryBuilder` interface.

Example Usage

```csharp
var calc = LoggingProxy<ICalculator>.Create(new Calculator(),
                                            new ConsoleLogger(),
                                            new LogEntryBuilder());

calc.Add(1, 2);
calc.Divide(12, 0);
```

If we decorate `T` with the `LoggingProxy` the logging will be totally automatic.

Sample output of `ConsoleLogger`

```
07/19/2019 19:32:35|Information|Invoke|ClassName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2
07/19/2019 19:32:35|Information|Leave|ClassName: AopLoggingConsole.Calculator, Method: Add, Args: 1, 2, Return type: System.Int32, Return value: 3
07/19/2019 19:32:36|Error|Exception|ClassName: AopLoggingConsole.Calculator, Method: Divide, Args: 12, 0, Exception: System.DivideByZeroException: Attempted to divide by zero.
   at AopLoggingConsole.Calculator.Divide(Int32 a, Int32 b) in /Users/otto/Source/GitHub/aop_logging/AopLoggingConsole/Calculator.cs:line 12

Unhandled Exception: System.DivideByZeroException: Attempted to divide by zero.
   at AopLogging.LoggingProxy`1.Invoke(MethodInfo targetMethod, Object[] args) in /Users/ottogal/Work/SideProjects/GitHub/aop_logging/AopLogging/LoggingProxy.cs:line 34
--- End of stack trace from previous location where exception was thrown ---
   at System.Reflection.DispatchProxyGenerator.Invoke(Object[] args)
   at generatedProxy_1.Divide(Int32 , Int32 )
   at AopLoggingConsole.Program.Main(String[] args) in /Users/otto/Source/GitHub/aop_logging/AopLoggingConsole/Program.cs:line 18

```
