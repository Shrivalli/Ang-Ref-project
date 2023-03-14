using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfferMicroservice.Models;
using PointsMicroservice.Models;
using PointsMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointsMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PointsController : ControllerBase
    {
        private readonly IPointService ser;
        public PointsController(IPointService _ser)
        {
            ser = _ser;
        }
        [HttpGet]
        [Route("GetPointsByEmployeeId/{employeeId}")]
        public Point GetPointsByEmployeeId(int employeeId, string token)
        {
            return ser.GetPointsByEmployeeId(employeeId, token);

        }
        [HttpGet]
        [Route("RefreshPointsByEmployee/{employeeId}")]
        public async Task<ActionResult> RefreshPointsByEmployee(int employeeId, string token)
        {
            List<Offer> newOffers = await ser.GetOfferList(token);
            var points = await ser.Refresh(employeeId, newOffers, token);
            return Ok(points);
        }
    }
}
