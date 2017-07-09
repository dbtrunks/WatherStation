using System;

namespace Data
{
    public interface IWatherStationRepository
    {
      WatherStation GetWatherStationByExternalKey(string externalKey);
    }
}