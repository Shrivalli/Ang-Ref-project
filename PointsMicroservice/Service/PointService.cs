using OfferMicroservice.Models;
using PointsMicroservice.Models;
using PointsMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointsMicroservice.Service
{
    public class PointService : IPointService
    {
        private readonly IPointRepo repo;
        public PointService(IPointRepo _repo)
        {
            repo = _repo;
        }

        public Task<List<Offer>> GetOfferList(string token)
        {
            return repo.GetOfferList(token);
        }

        public Point GetPointsByEmployeeId(int employeeId, string token)
        {
            return repo.GetPointsByEmployeeId(employeeId,token);
        }

        public Task<Point> Refresh(int employeeId, List<Offer> newOffer, string token)
        {
            return repo.Refresh(employeeId, newOffer,token);
        }
    }
}
