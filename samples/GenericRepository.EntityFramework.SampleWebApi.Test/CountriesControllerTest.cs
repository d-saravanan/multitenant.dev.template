using Moq;
using MultiTenancy.Core.ProviderContracts;
using MultiTenantRepository.EntityFramework;
using MultiTenantRepositry.EF.Api.Controllers;
using MultiTenantRepositry.EF.Api.Test.Infrastructure;
using MultiTenantRepositry.EF.Core.Entities;
using MultiTenantRepositry.EF.Core.Services;
using MultiTenantServices.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace MultiTenantRepositry.EF.Api.Test
{
    public class CountriesControllerTest
    {
        public CountriesControllerTest()
        {
            Global.RegisterMappings();
        }

        [Fact]
        public void GetCountries_Should_Get_Expected_Result()
        {

            // Arrange

            var countries = new List<Country>() {
                new Country { Id = 1, Name = "Turkey", ISOCode = "TR", CreatedOn = DateTimeOffset.Now },
                new Country { Id = 2, Name = "United Kingdom", ISOCode = "EN", CreatedOn = DateTimeOffset.Now },
                new Country { Id = 3, Name = "United States", ISOCode = "US", CreatedOn = DateTimeOffset.Now },
                new Country { Id = 4, Name = "Argentina", ISOCode = "AR", CreatedOn = DateTimeOffset.Now },
                new Country { Id = 4, Name = "Bahamas", ISOCode = "BS", CreatedOn = DateTimeOffset.Now },
                new Country { Id = 4, Name = "Uruguay", ISOCode = "UY", CreatedOn = DateTimeOffset.Now }
            };
            var dbSet = new FakeDbSet<Country>();
            foreach (var country in countries) {
                dbSet.Add(country);
            }

            var entitiesContext = new Mock<IEntitiesContext>();
            entitiesContext.Setup(ec => ec.Set<Country>()).Returns(dbSet);

            var userContext = getMockedUserContext();

            var countriesRepo = new MultiTenantRepository<Country>(entitiesContext.Object);
            MultiTenantServices<Country, int> countryService = new CountryService(countriesRepo);
            var countriesController = new CountriesController(countryService, Global._mapper, userContext);
            var pageIndex = 1;
            var pageSize = 3;

            // Act
            var paginatedDto = countriesController.GetCountries(pageIndex, pageSize).Result;

            // Assert
            Assert.NotNull(paginatedDto);
            Assert.Equal(countries.Count, paginatedDto.TotalCount);
            Assert.Equal(pageSize, paginatedDto.PageSize);
            Assert.Equal(pageIndex, paginatedDto.PageIndex);
        }

        private static IUserContextDataProvider getMockedUserContext()
        {
            var userContext = new Mock<IUserContextDataProvider>();
            userContext.Setup(uc => uc.TenantId).Returns(Guid.NewGuid());
            userContext.Setup(uc => uc.UserId).Returns(Guid.NewGuid());
            userContext.Setup(uc => uc.UserName).Returns(System.IO.Path.GetRandomFileName());
            return userContext.Object;
        }
    }
}