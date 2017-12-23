using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicBandsDb.Entities
{
    public class Band
    {
        public int BandId { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Song> Songs { get; set; }
        public virtual ICollection<Musician> Musicians { get; set; }
        public virtual ICollection<Performance> Performances { get; set; }

        public Band()
        {
            Songs = new List<Song>();
            Musicians = new List<Musician>();
            Performances = new List<Performance>();
        }
    }
}
