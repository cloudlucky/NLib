# UnitTestWorkerDriver class
Helper class to test web application

```csharp
using (var td = new UnitTestWorkerDriver())
{
    var response = td.GetResponse("MyPage.aspx", "Name=Foo");

    // code
}
```
