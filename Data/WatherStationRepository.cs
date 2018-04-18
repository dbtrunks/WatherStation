using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WatherStationRepository : IWatherStationRepository
    {
        readonly MyDbContext _dbContext;
        public WatherStationRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public WatherStation GetWatherStationByExternalKey(Guid externalKey)
        {
            var result = _dbContext.WatherStation.Where(x => x.ExternalKey == externalKey).FirstOrDefault();
            return result;
        }

        public void SaveTemperatureMeasurement(int watherStationID, decimal temperature)
        {
            var tm = new TemperatureMeasurement
            {
                Temperature = temperature,
                DateTime = DateTime.Now
            };
            tm.WatherStation = _dbContext.WatherStation.Where(x => x.Id == watherStationID).First();
            _dbContext.TemperatureMeasurement.Add(tm);

            _dbContext.SaveChanges();
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(Guid externalKey)
        {
            var result = TemperatureMeasurementQuery(externalKey, _dbContext).OrderByDescending(t => t.Id).FirstOrDefault();
            return result;
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(Guid externalKey, DateTime? date)
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

        public List<DateTime> GetTemperatureMeasurementsDates(Guid externalKey)
        {
            var result = TemperatureMeasurementQuery(externalKey, _dbContext).Select(s => s.DateTime.Date).GroupBy(g => g.Date).Select(grp => grp.FirstOrDefault()).ToList();
            return result;

        }

        public List<WatherStation> GetWatherStations()
        {

            var result = _dbContext.WatherStation.ToList();
            return result;

        }

        private static IQueryable<TemperatureMeasurement> TemperatureMeasurementQuery(Guid externalKey, MyDbContext db)
        {
            return db.TemperatureMeasurement.Include("WatherStation").Where(t => t.WatherStation.ExternalKey == externalKey);
        }
    }
}