# Check class

Check class is use to guard a method from incorrect parameters.

Example:

Common way
{code:c#}
    public void DoSomething(int param1)
    {
        if (param1 < 0)
        {
            throw new ArgumentException("My message");
        }

       // ...
    }
{code:c#}

New way
{code:c#}
    public void DoSomething(int param1)
    {
        Check.Requires<ArgumentException>(param1 >= 0, "My message");

       // ...
    }
{code:c#}

## Example for each methods

**{{Requires<TException>(bool condition)}}**
{code:c#}
Check.Requires<ArgumentException>(param1 >= 0);
{code:c#}

**{{Requires<TException>(bool condition, string message)}}**
{code:c#}
Check.Requires<ArgumentException>(param1 >= 0, "My message");
{code:c#}

**{{Requires<TException>(bool condition, object arguments)}}**
{code:c#}
Check.Requires<ArgumentException>(param1 >= 0, new { message = "My message", paramName = "param1" });
{code:c#}

**{{Requires<TException>(bool condition, string message, object arguments)}}**
{code:c#}
Check.Requires<ArgumentException>(param1 >= 0, "My message", new { paramName = "param1" });
{code:c#}

{{object argument}} is an anonymous object that represents the parameters of one exception's constructor. The order is not important.