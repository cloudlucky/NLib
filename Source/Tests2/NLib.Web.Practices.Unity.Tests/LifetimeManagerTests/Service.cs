namespace NLib.Web.Practices.Unity.Tests.LifetimeManagerTests
{
    public interface IService
    {
        string Name { get; }
    }

    public class Service : IService
    {
        public Service(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
