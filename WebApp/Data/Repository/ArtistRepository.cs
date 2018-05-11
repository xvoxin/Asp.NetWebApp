using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Repository.Interfaces;
using WebApp.Models;

namespace WebApp.Data.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ArtistRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public void Add(Artist artist)
        {
            _dbContext.Add(artist);
        }

        public void Delete(Artist artist)
        {
            _dbContext.Remove(artist);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _dbContext.Artist.ToListAsync();
        }

        public async Task<Artist> GetById(string id)
        {
            return await _dbContext.Artist.FirstAsync(x => x.ArtistID == Int64.Parse(id));
        }

        public void Update(Artist artist)
        {
            _dbContext.Update(artist);
        }
    }
}
