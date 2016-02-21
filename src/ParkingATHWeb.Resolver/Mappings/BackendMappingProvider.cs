namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        public static void InitMappings()
        {
            InitializeOrderMappings();
            InitializeGateUsageMappings();
            InitializePriceTresholdMappings();
            InitializeStudentMappings();
            InitializeTokenMappings();
            InitalizeMessageMappings();
            InitializeWeatherMappings();
        }
    }
}
