using CSMarketApp.Domain.Interfaces;
using Infrastructure.Data;
using Ninject.Modules;

namespace CSMarketApp.Algorithms
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersRepo>().To<UsersRepo>();
            Bind<IDealsRepo>().To<DealsRepo>();
            Bind<IItemsRepo>().To<ItemsRepo>();
        }
    }
}
