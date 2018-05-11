using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin, Employee, User")]
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index(string search)
        {
            var applicationDbContext = _context.Session
            .Include(p => p.Artist)
            .Include(p => p.Employee);

            ViewData["CurrentFilter"] = search;

            var sessions = from a in applicationDbContext select a;
            if (!String.IsNullOrEmpty(search))
            {
                sessions = sessions.Where(a => a.Artist.Name.Contains(search)
                                               || a.Employee.FullName.Contains(search));
            }

            return View(await sessions.AsNoTracking().ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .Include(p => p.Artist)
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            ViewData["ArtistID"] = new SelectList(_context.Artist, "ArtistID", "Name");
            ViewData["EmployeeID"] = new SelectList(
                (from e in _context.Employee
                    select new
                    {
                        e.EmployeeID,
                        FullName = e.Name + " " + e.Surname
                    }),
                "EmployeeID", "FullName", null);

            return View(session);
        }

        // GET: Sessions/Create
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artist, "ArtistID", "Name");
            ViewData["EmployeeID"] = new SelectList(
                (from e in _context.Employee
                    select new
                    {
                        e.EmployeeID,
                        FullName = e.Name + " " + e.Surname
                    }),
                "EmployeeID", "FullName", null);

            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionID,ArtistID,EmployeeID,SessionLength,SessionDate")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(session);
        }

        // GET: Sessions/Edit/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var session = await _context.Session.SingleOrDefaultAsync(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            ViewData["ArtistID"] = new SelectList(_context.Artist, "ArtistID", "Name");
            ViewData["EmployeeID"] = new SelectList(
                (from e in _context.Employee
                    select new
                    {
                        e.EmployeeID,
                        FullName = e.Name + " " + e.Surname
                    }),
                "EmployeeID", "FullName", null);

            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionID,ArtistID,EmployeeID,SessionLength,SessionDate")] Session session)
        {
            if (id != session.SessionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.SessionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(session);
        }

        // GET: Sessions/Delete/5
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .Include(p => p.Artist)
                .Include(p => p.Employee)
                .SingleOrDefaultAsync(m => m.SessionID == id);
            if (session == null)
            {
                return NotFound();
            }

            ViewData["ArtistID"] = new SelectList(_context.Artist, "ArtistID", "Name");
            ViewData["EmployeeID"] = new SelectList(
                (from e in _context.Employee
                    select new
                    {
                        e.EmployeeID,
                        FullName = e.Name + " " + e.Surname
                    }),
                "EmployeeID", "FullName", null);

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Session.SingleOrDefaultAsync(m => m.SessionID == id);
            _context.Session.Remove(session);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.SessionID == id);
        }
    }
}
