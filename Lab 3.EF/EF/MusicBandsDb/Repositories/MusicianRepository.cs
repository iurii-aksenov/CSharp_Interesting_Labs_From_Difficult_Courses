using System;
using System.Collections.Generic;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class MusicianRepository : IRepository<Musician>
    {
        public MusicianRepository(){}

        public void Create(Musician item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Musicians.Add(item);
                _db.SaveChanges();
            }
        }

        public Musician FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Musicians.Find(id);
            }
        }

        public IEnumerable<Musician> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Musicians.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Musician> Get(Func<Musician, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Musicians.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(Musician item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Delete(Musician item)
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
