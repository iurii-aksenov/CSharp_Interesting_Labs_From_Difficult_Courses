using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBandsDb.Entities;
using MusicBandsDb.Repositories;

namespace MusicBandsDb
{
    [Obsolete]
    public class UnitOfWork :IDisposable
    {
        private MusicBandContext _db = new MusicBandContext();
        private BandRepository _bandRepository;
        private MusicianRepository _musicianRepository;
        private MusicianProfileRepository _musicianProfilerepository;
        private PerformanceRepository _performanceRepository;
        private SongRepository _songRepository;
        private StatsRepository _statsRepository;

        public BandRepository Bands => _bandRepository ?? (_bandRepository = new BandRepository());

        public MusicianRepository Musicians => _musicianRepository ?? (_musicianRepository = new MusicianRepository());

        public MusicianProfileRepository MusicianProfiles => _musicianProfilerepository ?? (_musicianProfilerepository = new MusicianProfileRepository());

        public PerformanceRepository Performances => _performanceRepository ?? (_performanceRepository = new PerformanceRepository());

        public SongRepository Songs => _songRepository ?? (_songRepository = new SongRepository());

        public StatsRepository Stats => _statsRepository ?? (_statsRepository = new StatsRepository());

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}










































//public void UpdateStats()
//{

//    AddAllStats();
//    var stats = Stats.Get();

//    foreach (var statp in stats)
//    {
//        var stat = Stats.FindById(statp.StatsId);
//        if (stat.IsDeleted) continue;
//        if (Bands.FindById(stat.BandId).IsDeleted && !stat.IsDeleted)
//        {
//            stat.IsDeleted = true;
//            Stats.Update(stat);
//            continue;
//        }
//        stat.NumberOfMusicians = Bands.FindById(stat.BandId).Musicians.Count;
//        stat.NumberOfSongs = Bands.FindById(stat.BandId).Songs.Count;
//        stat.RoyaltiesForAllSongs = Bands.FindById(stat.BandId).Songs.Sum(x => x.Royalties);
//        if (Bands.FindById(stat.BandId).Songs.Any()) stat.AverageRoyalties = Bands.FindById(stat.BandId).Songs.Average(x => x.Royalties);
//        Stats.Update(stat);              
//    }
//}

//private void AddAllStats()
//{
//    var stats = Stats.Get();
//    var bands = Bands.Get();

//    if (bands.Count() > stats.Count())
//    {
//        var bandIdsInStats = stats.Select(x => x.BandId).ToList();
//        var bandsIds = bands.Select(x => x.BandId).ToList();

//        var exceptingIds = bandsIds.Except(bandIdsInStats).ToList();

//        foreach (var bandId in exceptingIds)
//        {
//            var stat = new Stats { BandId = bandId };
//            Stats.Create(stat);

//        }

//        Save();
//    }
//}
