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

        public bool CheckTemperatureRange(decimal Temperature)
        {
            if(-100.0M < Temperature && Temperature < 100.0M )
             return true;
            else
             return false;
        }

        public void SaveTemperatureMeasurement(string externalKey, decimal temperature)
        {
            var watherStation = _watherStationRepository.GetWatherStationByExternalKey(externalKey);
            if(watherStation == null)
                throw new ArgumentException(string.Format("Nie znaleziono stacji pogodowej o podanym kluczu {0}.",externalKey), "watherStation");
                
            if(!CheckTemperatureRange(temperature))
                throw new ArgumentException(string.Format("Podana teperatura {0} nie miesci sie w przyjętym zakresie. ",temperature));
        
                _watherStationRepository.SaveTemperatureMeasurement(watherStation, temperature);
        }
    }
}
