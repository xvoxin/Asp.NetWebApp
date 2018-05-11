using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data.Repository.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAll();
        Task<Artist> GetById(string id);
        void Add(Artist artist);
        void Update(Artist artist);
        void Delete(Artist artist);
    }
}
