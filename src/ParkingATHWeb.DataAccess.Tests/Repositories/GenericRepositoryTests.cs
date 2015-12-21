using Autofac.Extras.Moq;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Repositories;
using ParkingATHWeb.DataAccess.Tests.Base;
using ParkingATHWeb.Model;
using SharpTestsEx;
using System.Linq;
using Xunit;

namespace ParkingATHWeb.DataAccess.Tests.Repositories
{
    public class GenericRepositoryTests : DataAccessTestBase
    {
        private readonly UnitOfWork _uow;

        private readonly PriceTresholdRepository _repository;

        private readonly AutoMock _mock = AutoMock.GetLoose();

        public GenericRepositoryTests()
        {
            var context = new ParkingAthContext(true);
            _mock.Mock<IDatabaseFactory>().Setup(x => x.Get()).Returns(context);
            _repository = _mock.Create<PriceTresholdRepository>();
            _uow = _mock.Create<UnitOfWork>();

            _repository.Add(GetPriceTreshold());
            _repository.Add(GetPriceTreshold());
            _repository.Add(GetPriceTreshold());

            _uow.Commit();
        }

        [Fact]
        public void AddEntity_ThenResultIsValid()
        {
            //Before
            var preResult = _repository.GetAll().ToList();

            //Act
            _repository.Add(GetPriceTreshold());
            _uow.Commit();
            var result = _repository.GetAll().ToList();

            //Then
            result.Count.Should().Be.EqualTo(preResult.Count + 1);
        }

        [Fact]
        public void UpdateEntity_ThenResultIsValid()
        {
            //Before
            var entites = _repository.GetAll().ToList();
            var lastEntity = entites.Last();

            //Act
            lastEntity.MinCharges = 999;
            _repository.Edit(lastEntity);
            _uow.Commit();

            //Then
            var result = _repository.GetAll().ToList();
            result.Count.Should().Be.EqualTo(entites.Count);
            lastEntity.MinCharges.Should().Be.EqualTo(999);
        }

        [Fact]
        public void DeleteLastEntity_ThenResultIsValid()
        {
            //Before
            var entites = _repository.GetAll().ToList();
            var lastEntity = entites.Last();

            //Act
            _repository.Delete(lastEntity);
            _uow.Commit();

            //Then
            var result = _repository.GetAll().ToList();
            result.Count.Should().Be.EqualTo(entites.Count - 1);
        }
    }
}
