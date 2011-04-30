<%@ Page Language="C#" %>
<%@ Import Namespace="Microsoft.Practices.ServiceLocation" %>
<%@ Import Namespace="NLib.Practices.Unity.Tests.LifetimeManagerTests" %>
<html>
<head>
  <title>HttpRequestLifetimeManagerTest.aspx</title>
</head>
<body>
  <form runat="server">
    <div>
        HttpRequestLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("HttpRequestLifetimeManager").Name %>
        ContainerControlledLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("ContainerControlledLifetimeManager").Name%>
    </div>
  </form>
</body>
</html>