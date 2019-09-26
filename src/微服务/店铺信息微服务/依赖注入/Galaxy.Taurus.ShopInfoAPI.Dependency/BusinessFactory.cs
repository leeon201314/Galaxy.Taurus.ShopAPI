using Autofac;

namespace Galaxy.Taurus.ShopInfoAPI.Dependency
{
    public class BusinessFactory
    {
        public static object _padLock = new object();

        private static IContainer _container;

        private static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    lock (_padLock)
                    {
                        if (_container == null)
                        {
                            DependencyRegister dependencyRegister = new DependencyRegister();
                            _container = dependencyRegister.Register();
                        }
                    }
                }

                return _container;
            }
        }

        public static T GetBusiness<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
