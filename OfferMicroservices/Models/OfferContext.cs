using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Models
{
    public partial class OfferContext : DbContext
    {
        public OfferContext()
        {
        }

        public OfferContext(DbContextOptions<OfferContext> options)
         : base(options)
        {
        }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<LikeData> LikeDatas  { get; set; }


    }
}
