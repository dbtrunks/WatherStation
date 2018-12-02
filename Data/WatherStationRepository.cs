using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WeatherStationRepository : IWeatherStationRepository
    {
        readonly MyDbContext _dbContext;
        public WeatherStationRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WeatherStation GetWeatherStationByExternalKey(String externalKey)
        {
            var result = _dbContext.WeatherStation.Where(x => x.ExternalKey == externalKey).FirstOrDefault();
            return result;
        }

        public void SaveTemperatureMeasurement(int WeatherStationID, decimal temperature)
        {
            var tm = new TemperatureMeasurement
            {
                Temperature = temperature,
                DateTime = DateTime.Now
            };
            tm.WeatherStation = _dbContext.WeatherStation.Where(x => x.Id == WeatherStationID).First();
            _dbContext.TemperatureMeasurement.Add(tm);

            _dbContext.SaveChanges();
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(String externalKey)
        {
            var result = TemperatureMeasurementQuery(externalKey, _dbContext).OrderByDescending(t => t.Id).FirstOrDefault();
            return result;
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(String externalKey, DateTime? date)
        {

            List<TemperatureMeasurement> result;
            if (date.HasValue)
                result = TemperatureMeasurementQuery(externalKey, _dbContext).Where(t => t.DateTime.Date == date.Value.Date).OrderBy(x => x.DateTime).ToList();
            else
            {
                if (TemperatureMeasurementQuery(externalKey, _dbContext).FirstOrDefault() != null)
                {
                    var lastDate = TemperatureMeasurementQuery(externalKey, _dbContext).Select(l => l.DateTime.Date).LastOrDefault();
                    result = TemperatureMeasurementQuery(externalKey, _dbContext).Where(t => t.DateTime.Date == lastDate).OrderBy(x => x.DateTime).ToList();
                }
                else
                {
                    return new List<TemperatureMeasurement>();
                }
            }
            return result;

        }

        public List<DateTime> GetTemperatureMeasurementsDates(String externalKey)
        {
            var result = TemperatureMeasurementQuery(externalKey, _dbContext).Select(s => s.DateTime.Date).GroupBy(g => g.Date).Select(grp => grp.FirstOrDefault()).ToList();
            return result;

        }

        public List<WeatherStation> GetWeatherStations()
        {

            var result = _dbContext.WeatherStation.ToList();
            return result;

        }

        private static IQueryable<TemperatureMeasurement> TemperatureMeasurementQuery(String externalKey, MyDbContext db)
        {
            return db.TemperatureMeasurement.Include("WeatherStation").Where(t => t.WeatherStation.ExternalKey == externalKey);
        }
    }
}