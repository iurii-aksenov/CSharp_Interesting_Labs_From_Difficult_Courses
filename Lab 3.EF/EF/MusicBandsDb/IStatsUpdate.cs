using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicBandsDb
{
    public interface IStatsUpdate<TEntity> where TEntity : class
    {
        void StatsUpdate(TEntity entity);
    }
}
