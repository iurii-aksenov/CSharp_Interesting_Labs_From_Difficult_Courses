using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MusicBandsDb.Entities;

namespace MusicBandsDb.Repositories
{
    public class BandRepository : IRepository<Band>
    {
        public BandRepository() { }

        public void Create(Band item)
        {
            using (var _db = new MusicBandContext())
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        _db.Bands.Add(item);

                        _db.Stats.Add(new Stats()
                        {
                            Band = item
                        });
                        _db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public Band FindById(int id)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Bands.Find(id);
            }
        }

        public IEnumerable<Band> Get()
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Bands.AsNoTracking().ToList();
            }
        }

        public IEnumerable<Band> Get(Func<Band, bool> predicate)
        {
            using (var _db = new MusicBandContext())
            {
                return _db.Bands.AsNoTracking().Where(predicate).ToList();
            }
        }

        public void Update(Band item)
        {
            UpdateStats(item);
        }

        public void Delete(Band item)
        {
            item.IsDeleted = true;
            UpdateStats(item);
        }

        private void UpdateStats(Band item)
        {
            using (var _db = new MusicBandContext())
            {
                using (var transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        _db.Entry(item).State = System.Data.Entity.EntityState.Modified;

                        var stat = _db.Stats.FirstOrDefault(x => x.BandId == item.BandId);

                        if (item.IsDeleted && !stat.IsDeleted)
                        {
                            stat.IsDeleted = true;
                        }
                        stat.NumberOfMusicians = item.Musicians.Count;
                        stat.NumberOfSongs = item.Songs.Count;
                        stat.RoyaltiesForAllSongs = item.Songs.Sum(x => x.Royalties);
                        if (item.Songs.Any()) stat.AverageRoyalties = item.Songs.Average(x => x.Royalties);

                        _db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
