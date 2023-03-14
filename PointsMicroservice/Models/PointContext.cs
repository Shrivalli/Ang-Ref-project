using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointsMicroservice.Models
{
    public partial class PointContext : DbContext
    {
        public PointContext()
        {
        }

        public PointContext(DbContextOptions<PointContext> options)
         : base(options)
        {
        }
        public DbSet<Point> Points { get; set; }

    }
}
