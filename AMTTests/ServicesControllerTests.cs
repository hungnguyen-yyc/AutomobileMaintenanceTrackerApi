using System;
using AMTApi.Controllers;
using AMTApi.Models;
using AMTDll;
using AMTDll.Models;
using Xunit;
using Moq;
using Newtonsoft.Json;

namespace AMTTests
{
    public class ServicesControllerTests
    {
        ServicesController _controller;

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_provided_null_post_service()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();
            var _serviceValidation = new Mock<IServicesValidation>();
            _controller = new ServicesController(_serviceRepo.Object, _serviceValidation.Object, _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(null));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(null));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_create_or_update_service_on_post_put_when_provided_valid_post_service()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.NewGuid(),
                ServiceType = MaintenanceTypeEnum.BrakeChange,
                Cost = 12,
                Date = DateTime.Now,
                Note = "",
                Odometer = 0
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.False(result.IsError);
            Assert.False(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_provided_invalid_service_vehicle()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.NewGuid(),
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = 12,
                Date = DateTime.Now,
                Note = "",
                Odometer = 0
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_cost_lower_than_0()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.NewGuid(),
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = -1,
                Date = DateTime.Now,
                Note = "",
                Odometer = 0
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_odometer_lower_than_0()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.NewGuid(),
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = 0,
                Date = DateTime.Now,
                Note = "",
                Odometer = -1
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_given_invalid_vehicle_id()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = Guid.Empty,
                ProviderId = Guid.NewGuid(),
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = 0,
                Date = DateTime.Now,
                Note = "",
                Odometer = -1
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_put_when_given_invalid_provider_id()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.Empty,
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = 0,
                Date = DateTime.Now,
                Note = "",
                Odometer = -1
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_service_on_post_or_put_when_vehicle_odometer_smaller_than_service_odometer()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id, VehicleType = VehicleTypeEnum.Diesel, Odometer = 1000 } });

            var post = new PostServiceModel()
            {
                VehicleId = id,
                ProviderId = Guid.Empty,
                ServiceType = MaintenanceTypeEnum.BatteryChange,
                Cost = 0,
                Date = DateTime.Now,
                Note = "",
                Odometer = 0
            };

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_delete_service_on_delete_when_given_invalid_service_id()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _serviceRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceModel>() { new ServiceModel() { Id = id } });

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(Guid.NewGuid().ToString()));

            //Assert
            Assert.True(result.IsError);
        }

        [Fact]
        public void Should_delete_service_on_delete_when_given_valid_vehicle_id()
        {
            //Arrange
            var _serviceRepo = new Mock<IRepository<ServiceModel>>();
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _serviceRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceModel>() { new ServiceModel() { Id = id } });

            _controller = new ServicesController(_serviceRepo.Object, new ServicesValidation(), _vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(id.ToString()));

            //Assert
            _serviceRepo.Verify(mock => mock.Remove(It.IsAny<ServiceModel>()));
        }
    }
}
