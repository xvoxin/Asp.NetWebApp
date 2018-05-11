using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Data.Repository.Interfaces;
using WebApp.Models;

namespace WebAppTest.Mocks
{
    public class ArtistRepositoryMock : IArtistRepository
    {

        Dictionary<int, Artist> artists = new Dictionary<int, Artist>();

        public void Add(Artist artist)
        {
            if (artists.Count != 0)
            {
                var lastkey = artists[artists.Count].ArtistID;
                artist.ArtistID = lastkey + 1;
            }
            else
                artist.ArtistID = 1;

            artists.Add(artist.ArtistID, artist);
                
        }

        public void Delete(Artist artist)
        {
            artists.Remove(artist.ArtistID);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            var list = new List<Artist>();
            foreach (Artist artist in artists.Values)
            {
                list.Add(artist);
            }

            return await Task.FromResult(list);
        }

        public async Task<Artist> GetById(string id)
        {
            var artist = artists[Int32.Parse(id)];
            return await Task.FromResult(artist);
        }

        public void Update(Artist artist)
        {
            var id = artist.ArtistID;

            artists.Remove(id);
            artists.Add(id, artist);
        }
    }
}
