using Newtonsoft.Json;
using OfferMicroservice.Models;
using PointsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PointsMicroservice.Repository
{
    public class PointRepo : IPointRepo
    {
        private readonly PointContext db;
        public PointRepo(PointContext _db)
        {
            db = _db;
        }

        public async Task<List<Offer>> GetOfferList(string token)
        {
            List<Offer> offers = new List<Offer>();
            var client = new HttpClient();
            //client.BaseAddress = new Uri("https://offermicroservicessclassifieds.azurewebsites.net");
            client.BaseAddress = new Uri("https://localhost:44362/");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage res = await client.GetAsync("api/Offer/GetOffersList");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                offers = JsonConvert.DeserializeObject<List<Offer>>(results);
            }
            return offers;
        }
        public async Task<List<LikeData>> GetLikeData(string token)
        {
            List<LikeData> like = new List<LikeData>();
            var client = new HttpClient();
            //client.BaseAddress = new Uri("https://offermicroservicessclassifieds.azurewebsites.net");
            client.BaseAddress = new Uri("https://localhost:44362/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage res = await client.GetAsync("api/Offer/GetLikeData");

            if (res.IsSuccessStatusCode)
            {
                var results = res.Content.ReadAsStringAsync().Result;
                like = JsonConvert.DeserializeObject<List<LikeData>>(results);
            }
            return like;
        }


        public Point GetPointsByEmployeeId(int employeeId, string token)
        {
            Point p = db.Points.Where(c => c.EmployeeId == employeeId).SingleOrDefault();
            return p;
        }

        public async Task<Point> Refresh(int employeeId, List<Offer> newOffer ,string token)
        {
            int id, likes_two_days, totalPoints = 0;
            DateTime date;
            var employeeoffer = newOffer.Where(c => c.EmployeeId == employeeId).ToList();

            foreach (var e in employeeoffer)
            {
                id = e.OfferId;
                date = e.OpenedDate;
                likes_two_days = await LikeInTwoDaysCount(id, date, token);
                e.LikesInTwoDays = likes_two_days;
                TimeSpan engaggedDuration = e.EngagedDate - e.OpenedDate;
                if (e.LikesInTwoDays > 50)
                    totalPoints += 50;
                else if (e.LikesInTwoDays > 100)
                    totalPoints += 100;
                else if (e.Status == "Engaged" && engaggedDuration.TotalDays <= 2)
                {
                    totalPoints += 100;
                }
            }
            Point p = db.Points.FirstOrDefault(c => c.EmployeeId == employeeId);
            p.PointsGained += totalPoints;
            db.SaveChanges();
            return p;

        }
        public async Task<int> LikeInTwoDaysCount(int id, DateTime date, string token)
        {
            List<LikeData> offer = await GetLikeData(token);
            int count = offer.Where(c => c.LikeDate == date && c.OfferId == id).Count();
            int count1 = offer.Where(c => c.LikeDate == date.AddDays(1) && c.OfferId == id).Count();
            return count + count1;
        }

       
    }
    
}
