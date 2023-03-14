using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointsMicroservice.Models
{
    public class Point
    {
        [Key]
        public int PointId { get; set; }

        public int EmployeeId { get; set; }

        public int PointsGained { get; set; }


    }
}
