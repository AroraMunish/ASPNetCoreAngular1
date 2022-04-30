using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreatWorld.Data;
using GreatWorld.Models;

namespace GreatWorld.Controllers.Web
{
    public class StopsController : Controller
    {
        private readonly GreatWorldWithEFContext _context;

        public StopsController(GreatWorldWithEFContext context)
        {
            _context = context;
        }

        // GET: Stops
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Stops.ToListAsync());
        //}

        public async Task<IActionResult> Index(string id)
        {
            ViewBag.tripId = id;
            if (id == null)
            {
                return View(await _context.Stops.ToListAsync());
            }
            else
            {
                int tid = Convert.ToInt32(id);
                Trip trip = _context.Trips
                                    .Include(t => t.Stops)
                                    .FirstOrDefault(m => m.id == tid);
                ViewBag.TripName = trip.Name;
                List<Stop> stops = trip.Stops.ToList();
                return View(stops);
            }
        }


        // GET: Stops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .FirstOrDefaultAsync(m => m.id == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        // GET: Stops/Create
        //public IActionResult Create()
        //{
        //  return View();
        //}

        public IActionResult Create(int tripId)
        {
            Stop stop = new Stop() { Tripid = tripId };
            return View(stop);
        }

        // POST: Stops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tripid,id,Name,Longitude,Latitude,Arrival,Order")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = stop.Tripid });
            }
            return View(stop);
        }

        // GET: Stops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops.FindAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        // POST: Stops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Longitude,Latitude,Arrival,Order,Tripid")] Stop stop)
        {
            if (id != stop.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StopExists(stop.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = stop.Tripid });
            }
            return View(stop);
        }

        // GET: Stops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .FirstOrDefaultAsync(m => m.id == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        // POST: Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stop = await _context.Stops.FindAsync(id);
            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = stop.Tripid });
        }

        private bool StopExists(int id)
        {
            return _context.Stops.Any(e => e.id == id);
        }
    }
}
