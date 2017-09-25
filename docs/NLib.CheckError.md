# CheckError class
CheckError class offer short syntax over the [Check class](Check)

**{{ArgumentNullException(object param, string paramName)}}**
Throw {{ArgumentNullException}} if {{param}} is {{null}}
```csharp
CheckError.ArgumentNullException(param1, "param1");
```

**{{ArgumentNullException(object param, string paramName, string message)}}**
Throw {{ArgumentNullException}} if {{param}} is {{null}}
```csharp
CheckError.ArgumentNullException(param1, "param1", "My message");
```
