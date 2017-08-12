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
                var result =  db.WatherStation.Where(x => x.ExternalKey == externalKey).FirstOrDefault();
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
                var result = db.TemperatureMeasurement.Include("WatherStation").OrderByDescending(t => t.Id).FirstOrDefault(t => t.WatherStation.ExternalKey == externalKey);
                return result;
            }
        }

        public List<TemperatureMeasurement> GetTemperatureMeasurements(Guid externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result = db.TemperatureMeasurement.Include("WatherStation").Where(t => t.WatherStation.ExternalKey == externalKey).ToList();
                return result;
            }
        }
    }
}