﻿using System;
using Data;
using System.Collections.Generic;

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
            var result = _watherStationRepository.GetWatherStationByExternalKey(new Guid(externalKey));
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
            var watherStation = _watherStationRepository.GetWatherStationByExternalKey(new Guid(externalKey));
            if (watherStation == null)
                throw new ArgumentException(string.Format("Nie znaleziono stacji pogodowej o podanym kluczu {0}.", externalKey), "watherStation");

            if (!TemperatureRangeIsCorrect(temperature))
                throw new ArgumentException(string.Format("Podana teperatura {0} nie miesci sie w przyjętym zakresie. ", temperature));

            _watherStationRepository.SaveTemperatureMeasurement(watherStation.Id, temperature);
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(string externalKey)
        {
            return _watherStationRepository.GetLastTemperatureMeasurement(new Guid(externalKey));
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(string externalKey, DateTime? date)
        {
            return _watherStationRepository.GetTemperatureMeasurements(new Guid(externalKey), date);
        }

        public List<DateTime> GetTemperatureMeasurementsDates(string externalKey)
        {
            return _watherStationRepository.GetTemperatureMeasurementsDates(new Guid(externalKey));
        }
    }
}
