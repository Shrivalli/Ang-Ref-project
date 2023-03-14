using System;
using System.Collections.Generic;

#nullable disable

namespace OfferMicroservice.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Password { get; set; }
    }
}
