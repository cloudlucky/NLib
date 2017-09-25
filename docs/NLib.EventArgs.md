# EventArgs class
Represents a generic {{EventArgs}}

{code:c#}
new EventArgs<int>(3);
{code:c#}

Example:

{code:c#}
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
{code:c#}