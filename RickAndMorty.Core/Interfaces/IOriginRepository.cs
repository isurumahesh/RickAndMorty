using RickAndMorty.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Core.Interfaces
{
    public interface IOriginRepository
    {
        Task<Origin> GetOrigin(string url);
        Task<Origin> SaveOrigin(Origin origin);
    }
}
