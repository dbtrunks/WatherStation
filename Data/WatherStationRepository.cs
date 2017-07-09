using System;
using System.Linq;

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
    }
}