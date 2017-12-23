using System;
using System.Collections.Generic;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class MusicianProfileRepository : IRepository<MusicianProfile>
    {

        public MusicianProfileRepository(){}

        public void Create(MusicianProfile item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.MusicianProfiles.Add(item);
                _db.SaveChanges();
            }

        }

        public MusicianProfile FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.MusicianProfiles.Find(id);
            }
        }

        public IEnumerable<MusicianProfile> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.MusicianProfiles.AsNoTracking().ToList();
            }
        }

        public IEnumerable<MusicianProfile> Get(Func<MusicianProfile, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.MusicianProfiles.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(MusicianProfile item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Delete(MusicianProfile item)
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
