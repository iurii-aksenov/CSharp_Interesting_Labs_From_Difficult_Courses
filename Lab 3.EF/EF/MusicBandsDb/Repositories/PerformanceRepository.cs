using System;
using System.Collections.Generic;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class PerformanceRepository : IRepository<Performance>
    {
        public PerformanceRepository(){}

        public void Create(Performance item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Performances.Add(item);
                _db.SaveChanges();
            }
        }

        public Performance FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Performances.Find(id);
            }
        }

        public IEnumerable<Performance> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Performances.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Performance> Get(Func<Performance, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Performances.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(Performance item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Delete(Performance item)
        {
            using (var _db = new MusicBandContext())
            {
                item.IsDeleted = true;
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }
    }
}
