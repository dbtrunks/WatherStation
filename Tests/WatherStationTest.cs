using System;
using Xunit;
using Data;
using Business;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class WatherStationTest
    {
        DbContextOptionsBuilder<MyDbContext> optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

        public WatherStationTest()
        {
            optionsBuilder.UseSqlServer("Server=DBTRUNKS\\SQLEXPRESS;Database=WatherStation;Trusted_Connection=True;");
        }

        [Fact]
        public void WatherStationVerificationTest()
        {
            WatherStation watherStation = new WatherStation() { Name = "TestWatherStation", ExternalKey = new Guid() };

            Mock<IWatherStationRepository> mockWatherStationRepository = new Mock<IWatherStationRepository>();
            mockWatherStationRepository.Setup(x => x.GetWatherStationByExternalKey(watherStation.ExternalKey)).Returns(watherStation);
            var ws = new WeatherStationLogic(mockWatherStationRepository.Object);
            WatherStation results = ws.GetWatherStation(watherStation.ExternalKey.ToString());

            Assert.NotNull(results);
            Assert.Equal(watherStation, results);
        }

        [Fact]
        public void TemperatureRangeVerificationTest()
        {
            decimal temp1 = 21.7M;
            decimal temp2 = 121.8M;
            decimal temp3 = -100.7M;
            var ws = new WeatherStationLogic(new WatherStationRepository(new MyDbContext(optionsBuilder.Options)));
            bool results1 = ws.TemperatureRangeIsCorrect(temp1);
            bool results2 = ws.TemperatureRangeIsCorrect(temp2);
            bool results3 = ws.TemperatureRangeIsCorrect(temp3);

            Assert.True(results1);
            Assert.False(results2);
            Assert.False(results3);
        }

    }
}
