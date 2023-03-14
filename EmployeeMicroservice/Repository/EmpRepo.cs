using EmployeeMicroservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EmployeeMicroservice.Repository
{
    public class EmpRepo : IEmpRepo
    {
        private readonly EmployeeContext db;

        public EmpRepo(EmployeeContext _db)
        {
            db = _db;
        }
       

        public Employee GetEmployeeProfile(int employeeId)
        {
            Employee Employee = db.Employees.Where(c => c.EmployeeId == employeeId).SingleOrDefault();
            return Employee;
           
        }

        public async Task<IList<Offer>> ViewEmployeeOffers(int employeeId, string token)
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
            var employeeOffers = offers.Where(c => c.EmployeeId == employeeId).ToList();
           
            return employeeOffers;
        }

        public async Task<IList<Offer>> ViewMostLikedOffers(int employeeId, string token)
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
            var offer = (from c in offers where c.EmployeeId == employeeId orderby c.Likes descending select c).Take(3).ToList();

            return offer;
        }
    }
}
