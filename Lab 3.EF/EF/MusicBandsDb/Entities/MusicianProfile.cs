using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBandsDb.Entities
{
    public class MusicianProfile
    { 
        [Key]
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        public string NativeLanguage { get; set; }
        public string Birthplace { get; set; }
        public string Specialization { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Musician Musician { get; set; }
    }
}
