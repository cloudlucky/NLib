# Check class

Check class is use to guard a method from incorrect parameters.

Example:

Common way
```csharp
    public void DoSomething(int param1)
    {
        if (param1 < 0)
        {
            throw new ArgumentException("My message");
        }

       // ...
    }
```

New way
```csharp
    public void DoSomething(int param1)
    {
        Check.Requires<ArgumentException>(param1 >= 0, "My message");

       // ...
    }
```

## Example for each methods

**{{Requires<TException>(bool condition)}}**
```csharp
Check.Requires<ArgumentException>(param1 >= 0);
```

**{{Requires<TException>(bool condition, string message)}}**
```csharp
Check.Requires<ArgumentException>(param1 >= 0, "My message");
```

**{{Requires<TException>(bool condition, object arguments)}}**
```csharp
Check.Requires<ArgumentException>(param1 >= 0, new { message = "My message", paramName = "param1" });
```

**{{Requires<TException>(bool condition, string message, object arguments)}}**
```csharp
Check.Requires<ArgumentException>(param1 >= 0, "My message", new { paramName = "param1" });
```

{{object argument}} is an anonymous object that represents the parameters of one exception's constructor. The order is not important.
