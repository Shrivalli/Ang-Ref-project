using EmployeeMicroservice.Models;
using EmployeeMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMicroservice.Service
{
    public class EmpService : IEmpService
    {
        private readonly IEmpRepo repo;

        public EmpService(IEmpRepo _repo)
        {
            repo = _repo;
        }
        public Employee GetEmployeeprofile(int employeeId)
        {
            return repo.GetEmployeeProfile(employeeId);

        }

        public async Task<IList<Offer>> ViewEmployeeOffers(int employeeId, string token)
        {
            return await repo.ViewEmployeeOffers(employeeId, token);
        }

        public async Task<IList<Offer>> ViewMostLikedOffers(int employeeId, string token)
        {
            return await repo.ViewMostLikedOffers(employeeId, token);
        }
    }


}

