using System.Linq;
using Autofac;
using ParkingATHWeb.Business.Services;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Resolver.Modules
{
    public class ContractModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EntityService<,,>)).As(typeof(IEntityService<,>)).InstancePerLifetimeScope().PropertiesAutowired();

            var repositoryAssembly = typeof(UserService).Assembly;
            var serviceTypes = repositoryAssembly.GetTypes().Where(n => n.IsClass && typeof(IDependencyService).IsAssignableFrom(n));

            foreach (var st in serviceTypes)
            {
                builder.RegisterType(st).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            }

            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof (CustomExpressionVisitor<>))
                .As(typeof (ICustomExpressionVisitor<>))
                .InstancePerLifetimeScope();
        }
    }
}
