using Microsoft.EntityFrameworkCore;
using RickAndMorty.Core.Entities;
using RickAndMorty.Core.Interfaces;
using RickAndMorty.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Infrastructure.Repositories
{
    internal class OriginRepository : IOriginRepository
    {
        private readonly RickAndMortyDbContext dbContext;
        public OriginRepository(RickAndMortyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Origin> GetOrigin(string url)
        {
           var origin= await dbContext.Origins.FirstOrDefaultAsync(a => a.Url == url);
            return origin;
        }

        public async Task<Origin> SaveOrigin(Origin origin)
        {
            await dbContext.Origins.AddAsync(origin);
            await dbContext.SaveChangesAsync();
            return origin;
        }
    }
}
