using System;

namespace Data
{
    public class TemperatureMeasurement
    {
        public int Id {get; set;}
        public WatherStation WatherStation {get; set;} 
        public decimal Temperature {get; set;}
        public DateTime DateTime  {get; set;}
    }
}