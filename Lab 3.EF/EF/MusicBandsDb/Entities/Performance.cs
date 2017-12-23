using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicBandsDb.Entities
{
    public class Performance
    {
        public int PerformanceId { get; set; }
        [MaxLength(50)]
        public string PerformanceName { get; set; }
        public int Proceeds { get; set; }
        [MaxLength(50)]
        public string Place { get; set; }
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Band> Bands { get; set; }

        public Performance()
        {
            Bands = new List<Band>();
        }
    }
}
