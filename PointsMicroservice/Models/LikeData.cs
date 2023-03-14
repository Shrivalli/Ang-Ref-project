using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OfferMicroservice.Models
{
    public class LikeData
    {
        [Key]
        public int LikeId { get; set; }
        public int OfferId { get; set; }
        public DateTime LikeDate { get; set; }
    }
}
