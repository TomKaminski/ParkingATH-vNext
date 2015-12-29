using System.Linq;
using Autofac.Extras.Moq;
using Microsoft.Data.Entity;
using ParkingATHWeb.Business.Services;
using ParkingATHWeb.Business.Tests.Base;
using ParkingATHWeb.DataAccess;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Repositories;
using ParkingATHWeb.Model;
using ParkingATHWeb.Resolver.Mappings;
using SharpTestsEx;
using Xunit;

namespace ParkingATHWeb.Business.Tests.Services
{
    public class EntityServiceTests : BusinessTestBase
    {
        private PriceTresholdService _sut;
        private readonly AutoMock _mock = AutoMock.GetLoose();

        public EntityServiceTests()
        {
            InitContext();
            BackendMappingProvider.InitMappings();


            _sut.Create(GetPriceTreshold());
            _sut.Create(GetPriceTreshold());
            _sut.Create(GetPriceTreshold());
        }

        [Fact]
        public void AddEntity_ThenResultIsValid()
        {
            //Before
            var preResult = _sut.GetAll().Result.ToList();

            //Act
            _sut.Create(GetPriceTreshold());

            var result = _sut.GetAll().Result.ToList();

            //Then
            result.Count.Should().Be.EqualTo(preResult.Count + 1);
        }

        [Fact]
        public void UpdateEntity_ThenResultIsValid()
        {
            InitContext();
            //Before
            var entites = _sut.GetAll().Result.ToList();
            var lastEntity = entites.Last();

            //Act
            lastEntity.MinCharges = 999;
            _sut.Edit(lastEntity);

            //Then
            var result = _sut.GetAll().Result.ToList();
            result.Count.Should().Be.EqualTo(entites.Count);
            lastEntity.MinCharges.Should().Be.EqualTo(999);
        }

        [Fact]
        public void DeleteLastEntity_ThenResultIsValid()
        {
            InitContext();
            //Before
            var entites = _sut.GetAll().Result.ToList();
            var lastEntity = entites.Last();

            //Act
            _sut.Delete(lastEntity);

            //Then
            var result = _sut.GetAll().Result.ToList();
            result.Count.Should().Be.EqualTo(entites.Count - 1);
        }

        private void InitContext()
        {
            _mock.Mock<IDatabaseFactory>().Setup(x => x.Get()).Returns(GetContext());
            var repository = _mock.Create<PriceTresholdRepository>();
            var uow = _mock.Create<UnitOfWork>();

            _sut = new PriceTresholdService(uow, repository);
        }

        private ParkingAthContext GetContext()
        {
            var context = new ParkingAthContext(true);
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return context;
        }
    }
}
