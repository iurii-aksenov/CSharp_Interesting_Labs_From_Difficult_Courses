using System;
using System.Collections.Generic;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class StatsRepository : IRepository<Stats>
    {
        public StatsRepository(){}

        public void Create(Stats item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Stats.Add(item);
            }
        }

        public Stats FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Stats.Find(id);
            }
        }

        public Stats FindByBandId(int bandId)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Stats.Where(x => x.BandId == bandId).FirstOrDefault();
            }
        }

        public IEnumerable<Stats> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Stats.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Stats> Get(Func<Stats, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Stats.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(Stats item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(Stats item)
        {
            using (var _db = new MusicBandContext())
            {
                item.IsDeleted = true;
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            }
        }
    }
}
