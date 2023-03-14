using System;
using System.Collections.Generic;

#nullable disable

namespace OfferMicroservice.Models
{
    public partial class Offer
    {
        public int OfferId { get; set; }
        public int EmployeeId { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Details { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime EngagedDate { get; set; }
        public DateTime ClosedDate { get; set; }
        public int? Likes { get; set; }
        public DateTime? LikeDate { get; set; }
        public int LikesInTwoDays { get; internal set; }
    }
}
