using OfferMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Service
{
    public interface IOfferService
    {
        public IList<Offer> GetOffersList();
        public Offer GetOfferById(int id);
        public IList<Offer> GetOfferByCategory(string category);
        public IList<Offer> GetOfferByOpenedDate(DateTime openedDate);
        public IList<Offer> GetOfferByTopThreeLikes();
        public Offer PostOffer(Offer newOffer);
        public Offer EditOffer(Offer updatedOffer);
        public Offer EngageOffer(Offer offerDetails);
        public Offer LikeOffer(int offerid);
        public List<LikeData> LikeData();


    }
}
