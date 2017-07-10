using System;
using Xunit;
using Data;
using Business;
using Moq;
namespace Tests
{
    public class WatherStationTest
    {
        [Fact]
        public void WatherStationVerificationTest()
        {
            WatherStation watherStation = new WatherStation(){ Name= "TestWatherStation", ExternalKey = new Guid()};

            Mock<IWatherStationRepository> mockWatherStationRepository= new Mock<IWatherStationRepository>();
            mockWatherStationRepository.Setup(x=>x.GetWatherStationByExternalKey(watherStation.ExternalKey.ToString())).Returns(watherStation);
            var ws = new WeatherStationLogic(mockWatherStationRepository.Object);
            WatherStation results = ws.GetWatherStation(watherStation.ExternalKey.ToString());

            Assert.NotNull(results);//, "Nie znaleziono stacji pogodowej o podanym kluczu.");
            Assert.Equal(watherStation,results);
        }

        [Fact]
        public void TemperatureRangeVerificationTest()
        {
            decimal temp1 = 21.7M;
            decimal temp2 = 121.8M;
            decimal temp3 = -100.7M;
            var ws = new WeatherStationLogic(new WatherStationRepository());
            bool results1 = ws.CheckTemperatureRange(temp1);
            bool results2 = ws.CheckTemperatureRange(temp2);
            bool results3 = ws.CheckTemperatureRange(temp3);

            Assert.True(results1);
            Assert.False(results2);
            Assert.False(results3);
        }
    }
}
