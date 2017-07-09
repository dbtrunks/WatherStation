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
    }
}
