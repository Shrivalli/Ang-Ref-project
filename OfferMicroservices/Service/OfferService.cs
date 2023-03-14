using OfferMicroservice.Models;
using OfferMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Service
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepo repo;

        public OfferService(IOfferRepo _repo)
        {
            repo = _repo;
        }

        public IList<Offer> GetOfferByCategory(string category)
        {
            return repo.GetOfferByCategory(category);
        }

        public Offer GetOfferById(int id)
        {
            return repo.GetOfferById(id);
        }

        public IList<Offer> GetOfferByOpenedDate(DateTime openedDate)
        {
            return repo.GetOfferByOpenedDate(openedDate);
        }

        public IList<Offer> GetOfferByTopThreeLikes()
        {
            return repo.GetOfferByTopThreeLikes();
        }

        public IList<Offer> GetOffersList()
        {
            return repo.GetOffersList();
        }

        public Offer PostOffer(Offer newOffer)
        {
            return repo.PostOffer(newOffer);
        }
        public Offer EditOffer(Offer updatedOffer)
        {
            return repo.EditOffer(updatedOffer);
        }

        public Offer EngageOffer(Offer offerDetails)
        {
            return repo.EngageOffer(offerDetails);
        }

        public Offer LikeOffer(int offerid)
        {
            return repo.LikeOffer(offerid);
        }

        public List<LikeData> LikeData()
        {
            return repo.LikeData();
        }
    }
}
