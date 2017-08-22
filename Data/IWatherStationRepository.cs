using System;
using System.Collections.Generic;

namespace Data
{
    public interface IWatherStationRepository
    {
      WatherStation GetWatherStationByExternalKey(Guid externalKey);
      void SaveTemperatureMeasurement(int watherStationID, decimal temperature);
      TemperatureMeasurement GetLastTemperatureMeasurement(Guid externalKey);
      List<TemperatureMeasurement> GetTemperatureMeasurements(Guid externalKey, DateTime? date);
      List<DateTime> GetTemperatureMeasurementsDates(Guid externalKey);
    }
}