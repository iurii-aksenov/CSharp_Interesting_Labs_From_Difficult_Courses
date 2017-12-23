using System.ComponentModel.DataAnnotations;

namespace MusicBandsDb.Entities
{
    public class Song
    {
        public int SongId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public int Royalties { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int? BandId { get; set; }
        public virtual Band Band { get; set; }
    }
}
