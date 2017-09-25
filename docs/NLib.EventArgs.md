# EventArgs class
Represents a generic {{EventArgs}}

```csharp
new EventArgs<int>(3);
```

Example:

```csharp
class MyClass
{
    event EventHandler<EventArgs<int>> MyEventHandler;
    MyClass()
    {
        MyEventHandler += MyMethodHandler;
    }

    void MyMethodHandler(object sender, EventArgs<int> e)
    {
        // Do something with e.Value
    }
}
```
