using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class TemperatureMeasurement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}
        public WeatherStation WeatherStation {get; set;} 
        public decimal Temperature {get; set;}
        public DateTime DateTime  {get; set;}
    }
}