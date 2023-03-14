using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfferMicroservice.Models;
using OfferMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OfferMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService ser;
        public OfferController(IOfferService _ser)
        {
            ser = _ser;
        }

        [HttpGet]
        [Route("GetOffersList")]
        public IList<Offer> GetOffersList()
        {
            return ser.GetOffersList();
        }

        [HttpGet]
        [Route("GetOfferById/{id}")]
        public ActionResult<Offer> GetOfferById(int id)
        {
            return ser.GetOfferById(id);
        }

        [HttpGet]
        [Route("GetOfferByCategory/{category}")]
        public IList<Offer> GetOfferByCategory(string category)
        {
            return ser.GetOfferByCategory(category);
        }

        [HttpGet]
        [Route("GetOfferByOpenedDate/{openedDate}")]
        public IList<Offer> GetOfferByOpenedDate(DateTime openedDate)
        {
            return ser.GetOfferByOpenedDate(openedDate);

        }

        [HttpGet]
        [Route("GetOfferByTopThreeLikes")]
        public IList<Offer> GetOfferByTopThreeLikes()
        {
            return ser.GetOfferByTopThreeLikes();
        }

        [HttpPost]
        [Route("PostOffer")]
        public ActionResult<Offer> PostOffer(Offer newOffer)
        {
            var post = ser.PostOffer(newOffer);
            if (post.EmployeeId == 0 || post.Category == null || post.Details == null)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }

        }

        [HttpPut]
        [Route("EditOffer")]

        public ActionResult<Offer> EditOffer(Offer updatedOffer)
        {
            var offer = ser.EditOffer(updatedOffer);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }

            if (offer.ClosedDate > offer.EngagedDate && offer.Status != "Closed")
            {
                return BadRequest("Please update status to Closed");
            }

            return Ok();
        }

        [HttpPost]
        [Route("EngageOffer")]
        public ActionResult<IEnumerable<Offer>> EngageOffer(Offer offerDetails)
        {
            var offer = ser.EngageOffer(offerDetails);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }
            else if (offer.Status == "Engaged" || offer.Status == "Closed")
            {
                return BadRequest("Offer is either Engaged or Closed");
            }
            else
            {
                return Ok("Offer status updated to Engaged");
            }
        }
        [HttpPost]
        [Route("LikeOffer/{offerid}")]
        public ActionResult LikeOffer(int offerid)
        {
            var offer = ser.LikeOffer(offerid);
            if (offer == null)
            {
                return NotFound("Offer not found");
            }
            else
            {
                Console.WriteLine(offer.Likes);
                return Ok();
            }
        } 
        [HttpGet]
        [Route("GetLikeData")]
        public ActionResult<List<LikeData>> GetLikeData()
        {
           return  ser.LikeData();
           
        }

    }
}