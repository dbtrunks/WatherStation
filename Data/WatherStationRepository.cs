using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WatherStationRepository : IWatherStationRepository
    {
        public WatherStation GetWatherStationByExternalKey(Guid externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result = db.WatherStation.Where(x => x.ExternalKey == externalKey).FirstOrDefault();
                return result;
            }
        }

        public void SaveTemperatureMeasurement(int watherStationID, decimal temperature)
        {
            using (var db = new MyDbContext())
            {
                var tm = new TemperatureMeasurement
                {
                    Temperature = temperature,
                    DateTime = DateTime.Now
                };
                tm.WatherStation = db.WatherStation.Where(x => x.Id == watherStationID).First();
                db.TemperatureMeasurement.Add(tm);

                db.SaveChanges();
            }
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(Guid externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result = TemperatureMeasurementQuery(externalKey, db).OrderByDescending(t => t.Id).FirstOrDefault();
                return result;
            }
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(Guid externalKey, DateTime? date)
        {
            using (var db = new MyDbContext())
            {
                List<TemperatureMeasurement> result;
                if (date.HasValue)
                    result = TemperatureMeasurementQuery(externalKey, db).Where(t => t.DateTime.Date == date.Value.Date).OrderBy(x => x.DateTime).ToList();
                else
                {
                    var lastDate = TemperatureMeasurementQuery(externalKey, db).Select(l => l.DateTime.Date).LastOrDefault();
                    result = TemperatureMeasurementQuery(externalKey, db).Where(t => t.DateTime.Date == lastDate).OrderBy(x => x.DateTime).ToList();
                }
                return result;
            }
        }

        public List<DateTime> GetTemperatureMeasurementsDates(Guid externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result = TemperatureMeasurementQuery(externalKey, db).Select(s => s.DateTime.Date).GroupBy(g => g.Date).Select(grp => grp.FirstOrDefault()).ToList();
                return result;
            }
        }

        public List<WatherStation> GetWatherStations()
        {
            using (var db = new MyDbContext())
            {
                var result = db.WatherStation.ToList();
                return result;
            }
        }


        private static IQueryable<TemperatureMeasurement> TemperatureMeasurementQuery(Guid externalKey, MyDbContext db)
        {
            return db.TemperatureMeasurement.Include("WatherStation").Where(t => t.WatherStation.ExternalKey == externalKey);
        }
    }
}