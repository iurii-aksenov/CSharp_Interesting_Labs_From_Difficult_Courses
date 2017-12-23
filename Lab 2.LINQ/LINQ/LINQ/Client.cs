using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    public class Client
    {
        public int Id { get; }
        public int Year { get; }
        public int Month { get; }
        public int Duration { get; }

        public Client(int id, int year, int month, int duration)
        {
            Id = id;
            Year = year;
            Month = month;
            Duration = duration;
        }

    }
}
