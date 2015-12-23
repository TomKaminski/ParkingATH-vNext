using Autofac;
using Microsoft.Data.Entity;
using ParkingATHWeb.DataAccess;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model;

namespace ParkingATHWeb.Resolver.Modules
{
    public class EfModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}