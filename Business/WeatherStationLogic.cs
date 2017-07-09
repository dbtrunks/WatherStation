using System;
using Data;

namespace Business
{
    public class WeatherStationLogic
    {
        readonly IWatherStationRepository _watherStationRepository;
        public WeatherStationLogic(IWatherStationRepository watherStationRepository)
        {
            _watherStationRepository = watherStationRepository;
        }
        public WatherStation GetWatherStation(string externalKey)
        {
            var result = _watherStationRepository.GetWatherStationByExternalKey(externalKey);
            return result;// new WatherStation(){ Name= "TestWatherStation", ExternalKey = new Guid(externalKey)};
        }
    }
}
