using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Data.Repository.Interfaces;
using WebApp.Extensions.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class ArtistsController : Controller
    {
        //private readonly ApplicationDbContext _context;

        private readonly IArtistRepository _context;

        public ArtistsController(IArtistRepository context)
        {
            _context = context;
        }

        // GET: Artists
        public async Task<IActionResult> Index(string search)
        {
            ViewData["CurrentFilter"] = search;

            var artists = await _context.GetAll();

            if (!String.IsNullOrEmpty(search))
            {
                artists = artists.Where(a => a.Name.Contains(search)
                                             || a.City.Contains(search)
                                             || a.Genre.Contains(search)).ToList();
            }
            return View("Index", artists);
        }

        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }
            var artist = await _context.GetById(id.ToString());
            if (artist == null)
            {
                return View("NotFound");
            }

            return View("Details", artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtistID,Name,MembersCount,Genre,City,RegistrationDate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                NameHelper.ArtistFixedNames(artist);
                _context.Add(artist);
                return RedirectToAction(nameof(Index));
            }
            return View("Create", artist);
        }

        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var artist = await _context.GetById(id.ToString());
            if (artist == null)
            {
                return View("NotFound");
            }
            return View("Edit", artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtistID,Name,MembersCount,Genre,City,RegistrationDate")] Artist artist)
        {
            if (id != artist.ArtistID)
            {
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    NameHelper.ArtistFixedNames(artist);
                    _context.Update(artist);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArtistExists(artist.ArtistID))
                    {
                        return View("NotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", artist);
        }

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var artist = await _context.GetById(id.ToString());
            if (artist == null)
            {
                return View("NotFound");
            }

            return View("Delete", artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _context.GetById(id.ToString());
            _context.Delete(artist);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistExists(int id)
        {
            var artist = await _context.GetById(id.ToString());
            return artist == null;
        }
    }
}
