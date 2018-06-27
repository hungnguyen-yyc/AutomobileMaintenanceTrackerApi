using System;
using AMTDll;
using AMTDll.Models;
using Xunit;

namespace AMTTests
{
    public class ServicesValidationTests
    {
        [Fact]
        public void Should_succeed_on_validate_service_when_given_unrestricted_service_type(){
            //arrange
            var val = new ServicesValidation();

            //act
            var result = val.Validation(MaintenanceTypeEnum.BrakeChange, VehicleTypeEnum.Diesel);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void Should_fail_on_validate_service_when_given_invalid_service_to_vehicle()
        {
            //arrange
            var val = new ServicesValidation();

            //act
            var result = val.Validation(MaintenanceTypeEnum.BatteryChange, VehicleTypeEnum.Diesel);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void Should_fail_on_validate_service_when_given_valid_service_to_vehicle()
        {
            //arrange
            var val = new ServicesValidation();

            //act
            var result = val.Validation(MaintenanceTypeEnum.BatteryChange, VehicleTypeEnum.Electric);

            //assert
            Assert.True(result);
        }
    }
}
