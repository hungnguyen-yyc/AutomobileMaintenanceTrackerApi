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
    public class VehiclesControllerTests
    {
        VehiclesController _controller;

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_null_post_vehicle()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();
            var _vehicleValidation = new Mock<IServicesValidation>();
            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(null));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(null));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_create_or_update_vehicle_on_post_put_when_provided_valid_post_vehicle()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();
            _vehicleRepo.Setup(moq => moq.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Plate = "test" } });

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "test model",
                Odometer = 0,
                Plate = "test plate",
                VehicleType = VehicleTypeEnum.Electric,
                Year = 0,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.False(result.IsError);
            Assert.False(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_invalid_make()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = null,
                Model = "test model",
                Odometer = 0,
                Plate = "test plate",
                VehicleType = VehicleTypeEnum.Electric,
                Year = 0,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_invalid_model()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "",
                Odometer = 0,
                Plate = "test plate",
                VehicleType = VehicleTypeEnum.Electric,
                Year = 0,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_invalid_odometer()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "test model",
                Odometer = -1,
                Plate = "test plate",
                VehicleType = VehicleTypeEnum.Electric,
                Year = 0,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_invalid_Plate()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "test model",
                Odometer = 0,
                Plate = "",
                VehicleType = VehicleTypeEnum.Electric,
                Year = 0,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_vehicle_on_post_put_when_provided_invalid_year()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "test model",
                Odometer = 0,
                Plate = "test plate",
                VehicleType = VehicleTypeEnum.Electric,
                Year = -1,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToVehicleModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_vehicle_on_post_put_when_provided_vehicle_with_existing_plate()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();
            _vehicleRepo.Setup(moq => moq.Read()).Returns(new System.Collections.Generic.List<VehicleModel>(){new VehicleModel(){Plate = "test"}});
            var id = Guid.NewGuid();
            var post = new PostVehicleModel()
            {
                Make = "test make",
                Model = "test model",
                Odometer = 0,
                Plate = "test",
                VehicleType = VehicleTypeEnum.Electric,
                Year = -1,
            };

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));

            //Assert
            Assert.True(result.IsError);
        }

        [Fact]
        public void Should_not_delete_vehicle_on_delete_when_given_invalid_vehicle_id()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id } });

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(Guid.NewGuid().ToString()));

            //Assert
            Assert.True(result.IsError);
        }

        [Fact]
        public void Should_delete_vehicle_on_delete_when_given_valid_vehicle_id()
        {
            //Arrange
            var _vehicleRepo = new Mock<IRepository<VehicleModel>>();

            var id = Guid.NewGuid();
            _vehicleRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<VehicleModel>() { new VehicleModel() { Id = id } });

            _controller = new VehiclesController(_vehicleRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(id.ToString()));

            //Assert
            _vehicleRepo.Verify(mock => mock.Remove(It.IsAny<VehicleModel>()));
        }
    }
}
