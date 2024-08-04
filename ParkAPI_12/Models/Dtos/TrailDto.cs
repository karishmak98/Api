using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ParkAPI_12.Models.Trail;

namespace ParkAPI_12.Models.Dtos
{
    public class TrailDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        [Required]
        public string Elevation { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; }
        public NationalPark NationalPark { get; set; }
    }
}
