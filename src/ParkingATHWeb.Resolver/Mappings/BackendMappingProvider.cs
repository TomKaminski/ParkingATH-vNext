namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        public static void InitMappings()
        {
            BackendMappingProvider.InitializeOrderMappings();
            BackendMappingProvider.InitializeGateUsageMappings();
            BackendMappingProvider.InitializePriceTresholdMappings();
            BackendMappingProvider.InitializeStudentMappings();
            BackendMappingProvider.InitializeTokenMappings();
            BackendMappingProvider.InitalizeMessageMappings();
        }
    }
}
