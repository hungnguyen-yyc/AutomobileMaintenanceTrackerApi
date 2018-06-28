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
    public class ServiceProvidersControllerTests
    {
        ServiceProvidersController _controller;
        Mock<IRepository<ServiceModel>> _serviceRepo;

        public ServiceProvidersControllerTests()
        {
            _serviceRepo = new Mock<IRepository<ServiceModel>>();
        }

        [Fact]
        public void Should_not_create_or_update_provider_on_post_put_when_provided_null_post_provider()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();
            var _providerValidation = new Mock<IServicesValidation>();
            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(null));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(null));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_create_or_update_provider_on_post_put_when_provided_valid_post_provider()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            var post = new PostServiceProviderModel()
            {
                Address = "address",
                Phone = "2341",
                ShopName = "name"
            };

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceProviderModel()));

            //Assert
            Assert.False(result.IsError);
            Assert.False(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_provider_on_post_put_when_provided_invalid_address()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            var post = new PostServiceProviderModel()
            {
                Address = "",
                Phone = "2341",
                ShopName = "name"
            };

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceProviderModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_provider_on_post_put_when_provided_invalid_phone()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            var post = new PostServiceProviderModel()
            {
                Address = "asd",
                Phone = "",
                ShopName = "name"
            };

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceProviderModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_create_or_update_provider_on_post_put_when_provided_invalid_name()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            var post = new PostServiceProviderModel()
            {
                Address = "address",
                Phone = "test",
                ShopName = ""
            };

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Post(post));
            var result1 = JsonConvert.DeserializeObject<ResultModel>(_controller.Put(post.ToServiceProviderModel()));

            //Assert
            Assert.True(result.IsError);
            Assert.True(result1.IsError);
        }

        [Fact]
        public void Should_not_delete_provider_on_delete_when_given_invalid_provider_id()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            _providerRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceProviderModel>() { new ServiceProviderModel() { Id = id } });

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(Guid.NewGuid().ToString()));

            //Assert
            Assert.True(result.IsError);
        }

        [Fact]
        public void Should_not_delete_provider_on_delete_when_provider_being_used_in_service()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            _providerRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceProviderModel>() { new ServiceProviderModel() { Id = Guid.NewGuid() } });
            _serviceRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceModel> { new ServiceModel{ProviderId = id} });
            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(Guid.NewGuid().ToString()));

            //Assert
            Assert.True(result.IsError);
        }

        [Fact]
        public void Should_delete_provider_on_delete_when_given_valid_vehicle_id()
        {
            //Arrange
            var _providerRepo = new Mock<IRepository<ServiceProviderModel>>();

            var id = Guid.NewGuid();
            _providerRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceProviderModel>() { new ServiceProviderModel() { Id = id } });
            _serviceRepo.Setup(setup => setup.Read()).Returns(new System.Collections.Generic.List<ServiceModel> { new ServiceModel { ProviderId = Guid.NewGuid() } });

            _controller = new ServiceProvidersController(_providerRepo.Object, _serviceRepo.Object);

            //Act
            var result = JsonConvert.DeserializeObject<ResultModel>(_controller.Delete(id.ToString()));

            //Assert
            _providerRepo.Verify(mock => mock.Remove(It.IsAny<ServiceProviderModel>()));
        }
    }
}
