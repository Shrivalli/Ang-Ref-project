using EmployeeMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMicroservice.Repository
{
    public interface IEmpRepo
    {
        public Employee GetEmployeeProfile(int employeeId);
        public Task<IList<Offer>> ViewEmployeeOffers(int employeeId, string token);
        public Task<IList<Offer>> ViewMostLikedOffers(int employeeId, string token);



    }
}
