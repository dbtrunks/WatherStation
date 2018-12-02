using System;
using System.Collections.Generic;

namespace Data
{
    public interface IWeatherStationRepository
    {
      WeatherStation GetWeatherStationByExternalKey(String externalKey);
      void SaveTemperatureMeasurement(int WeatherStationID, decimal temperature);
      TemperatureMeasurement GetLastTemperatureMeasurement(String externalKey);
      List<TemperatureMeasurement> GetTemperatureMeasurements(String externalKey, DateTime? date);
      List<DateTime> GetTemperatureMeasurementsDates(String externalKey);
      List<WeatherStation> GetWeatherStations();
    }
}