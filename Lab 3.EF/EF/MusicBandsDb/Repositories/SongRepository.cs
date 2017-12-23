using System;
using System.Collections.Generic;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class SongRepository : IRepository<Song>
    {
        public SongRepository() { }

        public void Create(Song item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Songs.Add(item);
                _db.SaveChanges();
            }
        }

        public Song FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Songs.Find(id);
            }
        }

        public IEnumerable<Song> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Songs.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Song> Get(Func<Song, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Songs.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(Song item)
        {
            using (var _db = new MusicBandContext())
            {
                _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }
        }

        public void Delete(Song item)
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
