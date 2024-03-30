using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApp.Maui.Models
{
    [Table("cars")]
    public class Car :BaseEntity
    {
        [Unique]
        public  string Vin { get; set; }
        public  string Make { get; set; }
        public  string Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? ImageUrl { get; set; }
    }
}
