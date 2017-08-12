using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class WatherStation
    {   
        public int Id {get; set;}
        public string Name {get; set;}
        [Required]
        public Guid ExternalKey {get; set;}
    }
}