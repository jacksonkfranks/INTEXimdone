using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace INTEXimdone.Models
{
    public interface ICrashRepository
    {
        public IQueryable<Crash> Crashes { get; }
        public void SaveCrash(Crash c);
        public void AddCrash(Crash c);
        public void DeleteCrash(Crash c);
    }
}
