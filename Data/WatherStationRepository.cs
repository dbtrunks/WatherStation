using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WatherStationRepository : IWatherStationRepository
    {       
        public WatherStation GetWatherStationByExternalKey(string externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result =  db.WatherStation.Where(x => x.ExternalKey.ToString() == externalKey).FirstOrDefault();
                return result;
            }  
        }

        public void SaveTemperatureMeasurement(WatherStation watherStation, decimal temperature)
        {
            using (var db = new MyDbContext())
            {
                var temperatureMeasurement = db.Set<TemperatureMeasurement>();
                temperatureMeasurement.Add(new TemperatureMeasurement
                {
                    WatherStation = watherStation,
                    Temperature = temperature,
                    DateTime = DateTime.Now
                });

                db.SaveChanges();
            }  
        }

        public TemperatureMeasurement GetLastTemperatureMeasurement(string externalKey)
        {
            using (var db = new MyDbContext())
            {
                var result = db.TemperatureMeasurement.Include("WatherStation").OrderByDescending(t => t.Id).FirstOrDefault(t => t.WatherStation.ExternalKey.ToString() == externalKey);
                return result;
            }
        }
    }
}