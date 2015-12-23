using Autofac;
using BackendMappingProvider = ParkingATHWeb.Resolver.Mappings.BackendMappingProvider;

namespace ParkingATHWeb.Resolver.Modules
{
    public class MapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            BackendMappingProvider.InitMappings();
            base.Load(builder);
        }
    }
}
