using OfferMicroservice.Models;
using PointsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointsMicroservice.Service
{
   public interface IPointService
    {
        public Point GetPointsByEmployeeId(int employeeId, string token);
        public Task<List<Offer>> GetOfferList(string token);
        public Task<Point> Refresh(int employeeId, List<Offer> newOffer, string token);


    }
}
