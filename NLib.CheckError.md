# CheckError class
CheckError class offer short syntax over the [Check class](Check)

**{{ArgumentNullException(object param, string paramName)}}**
Throw {{ArgumentNullException}} if {{param}} is {{null}}
{code:c#}
CheckError.ArgumentNullException(param1, "param1");
{code:c#}

**{{ArgumentNullException(object param, string paramName, string message)}}**
Throw {{ArgumentNullException}} if {{param}} is {{null}}
{code:c#}
CheckError.ArgumentNullException(param1, "param1", "My message");
{code:c#}