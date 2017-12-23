using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBandsDb.Entities
{
    public class Stats
    {
        public int StatsId { get; set; }
        [ForeignKey("Band")]
        public int BandId { get; set; }

        public int NumberOfMusicians { get; set; } = 0;
        public int NumberOfSongs { get; set; } = 0;
        public double RoyaltiesForAllSongs { get; set; } = 0;
        public double AverageRoyalties { get; set; } = 0;

        public Band Band { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
