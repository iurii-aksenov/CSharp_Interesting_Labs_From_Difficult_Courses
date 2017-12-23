using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicBandsDb;
using MusicBandsDb.Entities;
using MusicBandsDb.Repositories;
using NUnit.Framework;

namespace MusicBandDbTests
{
    public class Tests
    {

        private IRepository<Band> _bands;
        private IRepository<MusicianProfile> _musicianProfiles;
        private IRepository<Musician> _musicians;
        private IRepository<Performance> _performances;
        private IRepository<Song> _songs;
        private IRepository<Stats> _stats;

        [SetUp]
        public void SetUp()
        {
            _bands = new BandRepository();
            _musicians = new MusicianRepository();
            _musicianProfiles = new MusicianProfileRepository();
            _performances = new PerformanceRepository();
            _songs = new SongRepository();
            _stats = new StatsRepository();
        }

        [Test]
        public void AddDeleteBandTest()
        {

            string time = DateTime.Now.ToString();
            Band band = new Band { Name = "Test_" + time };
            int trueResult = _stats.Get().Count() + 1;
            _bands.Create(band);
            Assert.AreEqual(band.Name, _bands.FindById(band.BandId).Name);
            

            Assert.AreEqual(trueResult, _stats.Get().Count());

            _bands.Delete(band);
            Assert.IsTrue(_bands.Get(x => x.Name == "Test_" + time).FirstOrDefault()?.IsDeleted);

        }

        [Test]
        public void AddDeletedMusicianTest()
        {      
            string time = DateTime.Now.ToString();
            Musician musician = new Musician { FirstName = "Test_" + time };

            _musicians.Create(musician);
            Assert.AreEqual(musician.FirstName, _musicians.FindById(musician.MusicianId).FirstName);

            _musicians.Delete(musician);
            Assert.IsTrue(_musicians.Get(x => x.FirstName == "Test_" + time).FirstOrDefault()?.IsDeleted);
        }

        [Test]
        public void AddMusicianProfileToMusicianTest()
        {
            string timeMusician = "Test_"+DateTime.Now.ToString();
            Musician musician = new Musician { FirstName = timeMusician };

            _musicians.Create(musician);
            Assert.AreEqual(musician.FirstName, _musicians.FindById(musician.MusicianId).FirstName);


            string timeProfile = "Test_"+DateTime.Now.ToString();
            MusicianProfile profile = new MusicianProfile {Specialization = timeProfile, Musician = musician };

            _musicianProfiles.Create(profile);
            Assert.AreEqual(profile.Specialization, _musicianProfiles.FindById(profile.MusicianId).Specialization);
            Assert.IsNotNull(_musicians.FindById(profile.MusicianId));


            _musicians.Delete(musician);
            _musicianProfiles.Delete(profile);
           
            Assert.IsTrue(_musicianProfiles.Get(x => x.Specialization == timeProfile).FirstOrDefault().IsDeleted);
        }

        [Test]
        public void UpdatingNumberOfMusiciansInBand()
        {
            string timeBand = "Test_"+DateTime.Now.ToString();
            Band band = new Band { Name = timeBand };

            _bands.Create(band);
            Assert.AreEqual(band.Name, _bands.FindById(band.BandId).Name);



            string timeMusician = "Test_" + DateTime.Now.ToString();
            Musician musician = new Musician { FirstName = timeMusician };

            _musicians.Create(musician);
            Assert.AreEqual(musician.FirstName, _musicians.FindById(musician.MusicianId).FirstName);

            int currentCountMusiciansInBand = _stats.Get(x=>x.BandId==band.BandId).FirstOrDefault().NumberOfMusicians;
            Assert.AreEqual(0, currentCountMusiciansInBand);

            band.Musicians.Add(musician);

            _bands.Update(band);


            int newCcurrentCountMusiciansInBand = _stats.Get(x=>x.BandId==band.BandId).FirstOrDefault().NumberOfMusicians;
            Assert.AreEqual(1, newCcurrentCountMusiciansInBand);

            _musicians.Delete(musician);
            Assert.IsTrue(_musicians.Get(x => x.FirstName == timeMusician).FirstOrDefault()?.IsDeleted);

            _bands.Delete(band);
            Assert.IsTrue(_bands.Get(x => x.Name == timeBand).FirstOrDefault()?.IsDeleted);
        }

        [TearDown]
        public void TearDown()
        {
           // uow.Dispose();
        }
    }
}
