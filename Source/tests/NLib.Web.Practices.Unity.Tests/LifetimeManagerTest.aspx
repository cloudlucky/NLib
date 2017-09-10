<%@ Page Language="C#" %>
<%@ Import Namespace="Microsoft.Practices.ServiceLocation" %>
<%@ Import Namespace="NLib.Web.Practices.Unity.Tests.LifetimeManagerTests" %>
<html>
<head>
  <title>HttpRequestLifetimeManagerTest.aspx</title>
</head>
<body>
  <form runat="server">
    <div>
        HttpRequestLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("HttpRequestLifetimeManager").Name %> <br />
        ContainerControlledLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("ContainerControlledLifetimeManager").Name %> <br />
        HttpApplicationLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("HttpApplicationLifetimeManager").Name%> <br />
        HttpSessionLifetimeManager: <%= ServiceLocator.Current.GetInstance<IService>("HttpSessionLifetimeManager").Name %> <br />
    </div>
  </form>
</body>
</html>