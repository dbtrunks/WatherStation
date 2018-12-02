using System;
using Data;
using System.Collections.Generic;

namespace Business
{
    public class WeatherStationLogic
    {
        readonly IWeatherStationRepository _weatherStationRepository;
        public WeatherStationLogic(IWeatherStationRepository weatherStationRepository)
        {
            _weatherStationRepository = weatherStationRepository;
        }
        public WeatherStation GetWeatherStation(string externalKey)
        {
            var result = _weatherStationRepository.GetWeatherStationByExternalKey(externalKey);
            return result;
        }

        public bool TemperatureRangeIsCorrect(decimal Temperature)
        {
            if (-100.0M < Temperature && Temperature < 100.0M)
                return true;
            else
                return false;
        }

        public void SaveTemperatureMeasurement(string externalKey, decimal temperature)
        {
            var weatherStation = _weatherStationRepository.GetWeatherStationByExternalKey(externalKey);
            if (weatherStation == null)
                throw new ArgumentException(string.Format("Nie znaleziono stacji pogodowej o podanym kluczu {0}.", externalKey), "weatherStation");

            if (!TemperatureRangeIsCorrect(temperature))
                throw new ArgumentException(string.Format("Podana teperatura {0} nie miesci sie w przyjętym zakresie. ", temperature));

            _weatherStationRepository.SaveTemperatureMeasurement(weatherStation.Id, temperature);
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(string externalKey)
        {
            return _weatherStationRepository.GetLastTemperatureMeasurement(externalKey);
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(string externalKey, DateTime? date)
        {
            return _weatherStationRepository.GetTemperatureMeasurements(externalKey, date);
        }

        public List<DateTime> GetTemperatureMeasurementsDates(string externalKey)
        {
            return _weatherStationRepository.GetTemperatureMeasurementsDates(externalKey);
        }

        public List<WeatherStation> GetWeatherStations()
        {
            return _weatherStationRepository.GetWeatherStations();
        }
    }
}
