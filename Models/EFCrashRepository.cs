using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEXimdone.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext context { get; set; }

        public EFCrashRepository(CrashDbContext temp)
        {
            context = temp;
        }
        public IQueryable<Crash> Crashes => context.Crashes;

        public void AddCrash(Crash c)
        {
            context.Add(c);
            context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            context.Remove(c);
            context.SaveChanges();
        }

        public void SaveCrash(Crash c)
        {
            context.Update(c);
            context.SaveChanges();
        }
    }
}
