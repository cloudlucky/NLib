# UnitTestWorkerDriver class
Helper class to test web application

{code:c#}
using (var td = new UnitTestWorkerDriver())
{
    var response = td.GetResponse("MyPage.aspx", "Name=Foo");

    // code
}
{code:c#}