using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicBandsDb.Entities
{
    public class Musician
    {
        public int MusicianId { get; set; }
        [MaxLength(30)]
        public string FirstName { get; set; }
        [MaxLength(30)]
        public string SecondName { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Band> Bands { get; set; }
        public MusicianProfile Profile { get; set; }

        public Musician()
        {
            Bands = new List<Band>();
        }
    }
}
