
using Centra.DataModelInfrastructure;
using DataModelSQL;
using Ninject.Modules;

namespace Core.Helper
{
    /// <summary>
    /// public class to manage dependency.
    /// </summary>
    public class DependencyResolver : NinjectModule
    {
        /// <summary>
        /// Function to load dependency.
        /// </summary>
        public override void Load()
        {
            Bind<IShopBridgeConfiguration>().To<ShopBridgeConfigurationDBManager>().InSingletonScope();
        }
    }
}