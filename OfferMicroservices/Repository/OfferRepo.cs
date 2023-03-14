using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Repository
{
    public class OfferRepo : IOfferRepo
    {
        private readonly OfferContext db;

        public OfferRepo(OfferContext _db)
        {
            db = _db;
        }
        public IList<Offer> GetOffersList()
        {
            return db.Offers.ToList();
        }
       

        public Offer GetOfferById(int id)
        {
            Offer offer = db.Offers.Where(c => c.OfferId == id).SingleOrDefault();
            return offer;
        }
        public IList<Offer> GetOfferByCategory(string category)
        {
            return db.Offers.Where(c => c.Category == category).ToList();
        }

        public IList<Offer> GetOfferByOpenedDate(DateTime openedDate)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(openedDate, culture);
            DateTime start = tempDate.Date;
            return db.Offers.Where(c => c.OpenedDate == start).ToList();
        }

        public IList<Offer> GetOfferByTopThreeLikes()
        {
            return db.Offers.OrderByDescending(c=>c.Likes).Take(3).ToList();
        }

      

        public Offer PostOffer(Offer newOffer)
        {
            db.Offers.Add(newOffer);
            db.SaveChanges();
            return newOffer;
        }
        public Offer EditOffer(Offer updatedOffer)
        {
            Offer offer = db.Offers.FirstOrDefault(c => c.OfferId == updatedOffer.OfferId && c.EmployeeId == updatedOffer.EmployeeId);
            if (offer != null)
            {
                offer.ClosedDate = updatedOffer.ClosedDate;
                offer.EngagedDate = updatedOffer.EngagedDate;

                offer.Status = updatedOffer.Status;

                offer.Details = updatedOffer.Details;

                offer.Category = updatedOffer.Category;
            }

            db.SaveChanges();
            return offer;
           
            }

        public Offer EngageOffer(Offer offerDetails)
        {
            Offer offer = db.Offers.FirstOrDefault(c => c.OfferId == offerDetails.OfferId && c.EmployeeId == offerDetails.EmployeeId);
            offer.Status = "Engaged";
            offer.EngagedDate = DateTime.Now;
            db.SaveChanges();
            return offer;
        }

        public Offer LikeOffer(int offerid)
        {

            Offer offer = db.Offers.FirstOrDefault(c => c.OfferId == offerid);
            offer.Likes = offer.Likes + 1;
            offer.LikeDate = DateTime.Now;
            db.SaveChanges();
            LikeData l = new LikeData();
            l.OfferId = offer.OfferId;
            l.LikeDate = DateTime.Now;
            db.LikeDatas.Add(l);
            db.SaveChanges();
            return offer;
        
        }

        public List<LikeData> LikeData()
        {
            return db.LikeDatas.ToList();
        }
    }    
}