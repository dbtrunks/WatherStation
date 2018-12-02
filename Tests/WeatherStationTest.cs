using System;
using Xunit;
using Data;
using Business;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class WeatherStationTest
    {
        DbContextOptionsBuilder<MyDbContext> optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();

        public WeatherStationTest()
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WeatherStation;Trusted_Connection=True;");
        }

        [Fact]
        public void WeatherStationVerificationTest()
        {
            WeatherStation WeatherStation = new WeatherStation() { Name = "TestWeatherStation", ExternalKey = "ED234AC345AT1" };

            Mock<IWeatherStationRepository> mockWeatherStationRepository = new Mock<IWeatherStationRepository>();
            mockWeatherStationRepository.Setup(x => x.GetWeatherStationByExternalKey(WeatherStation.ExternalKey)).Returns(WeatherStation);
            var ws = new WeatherStationLogic(mockWeatherStationRepository.Object);
            WeatherStation results = ws.GetWeatherStation(WeatherStation.ExternalKey.ToString());

            Assert.NotNull(results);
            Assert.Equal(WeatherStation, results);
        }

        [Fact]
        public void TemperatureRangeVerificationTest()
        {
            decimal temp1 = 21.7M;
            decimal temp2 = 121.8M;
            decimal temp3 = -100.7M;
            var ws = new WeatherStationLogic(new WeatherStationRepository(new MyDbContext(optionsBuilder.Options)));
            bool results1 = ws.TemperatureRangeIsCorrect(temp1);
            bool results2 = ws.TemperatureRangeIsCorrect(temp2);
            bool results3 = ws.TemperatureRangeIsCorrect(temp3);

            Assert.True(results1);
            Assert.False(results2);
            Assert.False(results3);
        }

    }
}
