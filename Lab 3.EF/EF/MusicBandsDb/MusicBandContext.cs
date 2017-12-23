using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBandsDb.Entities;

namespace MusicBandsDb
{
    public class MusicBandContext : DbContext
    {
        static MusicBandContext()
        {
           // Database.SetInitializer<MusicBandContext>(new MusicBandContextInitializaer());
        }
        public MusicBandContext() : base("MusicBandDb") { }

        public DbSet<Band> Bands { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicianProfile> MusicianProfiles { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Stats> Stats { get; set; }

    }

    class MusicBandContextInitializaer : DropCreateDatabaseAlways<MusicBandContext>
    {
        protected override void Seed(MusicBandContext db)
        {
            Musician musician1 = new Musician { MusicianId = 1, FirstName = "Steve", SecondName = "Jobs" };
            Musician musician2 = new Musician { MusicianId = 2, FirstName = "Anronio", SecondName = "Vivaldi" };
            Musician musician3 = new Musician { MusicianId = 3, FirstName = "Sebastian", SecondName = "Bah" };
            Musician musician4 = new Musician { MusicianId = 4, FirstName = "Ludvig", SecondName = "Bethoven" };

            Band band1 = new Band { BandId = 1, Name = "Classics" };
            Band band2 = new Band { BandId = 2, Name = "IT-Classic" };

            Song song1 = new Song { SongId = 1, Name = "Apple", Royalties = 10_000 };
            Song song2 = new Song { SongId = 2, Name = "Trio", Royalties = 10_354 };
            Song song3 = new Song { SongId = 2, Name = "Осень", Royalties = 125 };

            MusicianProfile musicianProfile1 =
                new MusicianProfile { MusicianId = 1, Birthplace = "USA", NativeLanguage = "C", Specialization = "IT" };
            MusicianProfile musicianProfile2 =
                new MusicianProfile
                {
                    MusicianId = 2,
                    Birthplace = "Frnace",
                    NativeLanguage = "Francisca",
                    Specialization = "Piano"
                };
            MusicianProfile musicianProfile3 =
                new MusicianProfile
                {
                    MusicianId = 3,
                    NativeLanguage = "Espanola",
                    Birthplace = "Espania",
                    Specialization = "Violin"
                };

            band1.Musicians.Add(musician1);

            band2.Musicians.Add(musician2);
            band2.Musicians.Add(musician3);

            band1.Songs.Add(song1);
            band2.Songs.Add(song2);
            band2.Songs.Add(song3);

           

            db.Musicians.AddRange(new List<Musician>() {musician1, musician2, musician3, musician4});
            
            db.MusicianProfiles.AddRange(
                new List<MusicianProfile> {musicianProfile1, musicianProfile2, musicianProfile3});

            db.Songs.AddRange(new List<Song> {song1, song2, song3});

            db.Bands.AddRange(new List<Band> { band1, band2 });

            Stats stats1 = new Stats
            {
                StatsId = 1,
                BandId = 1,
                NumberOfMusicians = band1.Musicians.Count,
                NumberOfSongs = band1.Songs.Count,
                RoyaltiesForAllSongs = band1.Songs.Sum(x => x.Royalties),
                AverageRoyalties = band1.Songs.Average(x => x.Royalties)
            };

            Stats stats2 = new Stats
            {
                StatsId = 2,
                BandId = 2,
                NumberOfMusicians = band2.Musicians.Count,
                NumberOfSongs = band2.Songs.Count,
                RoyaltiesForAllSongs = band2.Songs.Sum(x => x.Royalties),
                AverageRoyalties = band2.Songs.Average(x => x.Royalties)
            };

            db.Stats.Add(stats1);
            db.Stats.Add(stats2);

            db.SaveChanges();

        }
    }
}
