using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;


namespace INTEXimdone.Models
{
    public class CrashDbContext : DbContext
    {
        public CrashDbContext(DbContextOptions<CrashDbContext> options) : base(options)
        {

        }

        public DbSet<Crash> Crashes { get; set; }
    }

    //public class RDSContext : DbContext
    //{
    //    public RDSContext()
    //      : base(GetRDSConnectionString())
    //    {
    //    }

    //    public static RDSContext Create()
    //    {
    //        return new RDSContext();
    //    }

   
    //}
}
