using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Mission13.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {

        private BowlersDbContext context { get; set; }
        public EFBowlerRepository ( BowlersDbContext temp)
        {
            context = temp;
        }

        public IQueryable<Bowler> Bowlers => context.Bowlers;
        public IQueryable<Team> Teams => context.Teams;

        public void SaveBowler(Bowler b)
        {
            context.Update(b);
            context.SaveChanges();
        }

        public void CreateBowler(Bowler b)
        {
            context.Add(b);
            context.SaveChanges();
        }

        public void DeleteBowler(Bowler b)
        {
            context.Remove(b);
            context.SaveChanges();
        }

        public int GetMaxID()
        {
            int maxID;
            maxID = context.Bowlers.Max(p => p.BowlerID);
            return maxID;
        }


    }
}
